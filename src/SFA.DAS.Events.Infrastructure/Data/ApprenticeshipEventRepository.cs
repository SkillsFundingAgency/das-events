using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Logging;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Infrastructure.Data
{
    public sealed class ApprenticeshipEventRepository : BaseRepository, IApprenticeshipEventRepository
    {
        protected override string TableName => "ApprenticeshipEvents";
        private readonly IEventsLogger _logger;

        public ApprenticeshipEventRepository(string databaseConnectionString, IEventsLogger logger) : base(databaseConnectionString)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            _logger = logger;
        }

        public async Task Create(ApprenticeshipEvent @event)
        {
            await WithTransaction(
                async (c, t) =>
                    {
                        var eventId = (await c.QueryAsync<long>(
                                $"INSERT INTO [dbo].[{TableName}](Event, CreatedOn, ApprenticeshipId, PaymentStatus, AgreementStatus, ProviderId, LearnerId, EmployerAccountId, TrainingType, TrainingId, TrainingStartDate, TrainingEndDate, TrainingTotalCost, PaymentOrder, LegalEntityId, LegalEntityName, LegalEntityOrganisationType, EffectiveFrom, EffectiveTo, DateOfBirth) "
                                + $"VALUES (@event, @createdOn, @apprenticeshipId, @paymentStatus, @agreementStatus, @providerId, @learnerId, @employerAccountId, @trainingType, @trainingId, @trainingStartDate, @trainingEndDate, @trainingTotalCost, @paymentOrder, @legalEntityId, @legalEntityName, @legalEntityOrganisationType, @effectiveFrom, @effectiveTo, @dateOfBirth);"
                                + "SELECT CAST(SCOPE_IDENTITY() as int)",
                                @event, transaction: t)).Single();

                        if (@event.PriceHistory != null && @event.PriceHistory.Any())
                        {
                            foreach (var priceHistory in @event.PriceHistory)
                            {
                                priceHistory.ApprenticeshipEventsId = eventId;
                                await c.ExecuteAsync("INSERT INTO [dbo].[PriceHistory](apprenticeshipEventsId, TotalCost, EffectiveFrom, EffectiveTo) "
                                        + "VALUES (@apprenticeshipEventsId, @totalCost, @effectiveFrom, @effectiveTo);",
                                        priceHistory, t, commandType: CommandType.Text);
                            }

                        }
                    }
                );
        }

        public async Task BulkUploadApprenticeshipEvents(IList<ApprenticeshipEvent> apprenticeshipEvents)
        {
            _logger.Debug($"Bulk uploading {apprenticeshipEvents.Count} apprenticeship events");

            var sw = Stopwatch.StartNew();
            var table = BuildApprenticeshipEventsDataTable(apprenticeshipEvents);
            _logger.Trace($"Building events data table took {sw.ElapsedMilliseconds}");

            sw = Stopwatch.StartNew();
            await WithConnection(async x =>
            {
                using (var bulkCopy = new SqlBulkCopy(x))
                {
                    bulkCopy.DestinationTableName = "[dbo].[ApprenticeshipEvents]";
                    bulkCopy.ColumnMappings.Add("Event", "Event");
                    bulkCopy.ColumnMappings.Add("CreatedOn", "CreatedOn");
                    bulkCopy.ColumnMappings.Add("ApprenticeshipId", "ApprenticeshipId");
                    bulkCopy.ColumnMappings.Add("PaymentOrder", "PaymentOrder");
                    bulkCopy.ColumnMappings.Add("PaymentStatus", "PaymentStatus");
                    bulkCopy.ColumnMappings.Add("AgreementStatus", "AgreementStatus");
                    bulkCopy.ColumnMappings.Add("ProviderId", "ProviderId");
                    bulkCopy.ColumnMappings.Add("LearnerId", "LearnerId");
                    bulkCopy.ColumnMappings.Add("EmployerAccountId", "EmployerAccountId");
                    bulkCopy.ColumnMappings.Add("TrainingType", "TrainingType");
                    bulkCopy.ColumnMappings.Add("TrainingId", "TrainingId");
                    bulkCopy.ColumnMappings.Add("TrainingStartDate", "TrainingStartDate");
                    bulkCopy.ColumnMappings.Add("TrainingEndDate", "TrainingEndDate");
                    bulkCopy.ColumnMappings.Add("TrainingTotalCost", "TrainingTotalCost");
                    bulkCopy.ColumnMappings.Add("LegalEntityId", "LegalEntityId");
                    bulkCopy.ColumnMappings.Add("LegalEntityName", "LegalEntityName");
                    bulkCopy.ColumnMappings.Add("LegalEntityOrganisationType", "LegalEntityOrganisationType");
                    bulkCopy.ColumnMappings.Add("EffectiveFrom", "EffectiveFrom");
                    bulkCopy.ColumnMappings.Add("EffectiveTo", "EffectiveTo");
                    bulkCopy.ColumnMappings.Add("DateOfBirth", "DateOfBirth");
                    await bulkCopy.WriteToServerAsync(table);
                    return 0;
                }
            });
            _logger.Trace($"Inserting events in to database took {sw.ElapsedMilliseconds}");
        }

        public async Task<IEnumerable<ApprenticeshipEvent>> GetByRange(DateTime fromDate, DateTime toDate, int pageSize, int pageNumber, long fromEventId)
        {
            return await WithConnection<IEnumerable<ApprenticeshipEvent>>(async c =>
            {
                var offset = pageSize * (pageNumber - 1);

                var parameters = new DynamicParameters();
                var sql = string.Empty;

                if (fromEventId > 0)
                {
                    parameters.Add("@fromEventId", fromEventId);

                    sql =
                        $"SELECT * FROM [dbo].[ApprenticeshipEvents] a " +
                        $"INNER JOIN [dbo].[PriceHistory] p on p.ApprenticeshipEventsId = a.Id " +
                        $"WHERE a.Id >= @fromEventId ORDER BY a.Id " +
                        $"OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY;";
                }
                else
                {
                    parameters.Add("@fromDate", fromDate);
                    parameters.Add("@toDate", toDate);

                    sql =
                        $"SELECT * FROM [dbo].[ApprenticeshipEvents] a " +
                        $"INNER JOIN [dbo].[PriceHistory] p on p.ApprenticeshipEventsId = a.Id " +
                        $" WHERE a.CreatedOn >=  @fromDate AND a.CreatedOn < @toDate ORDER BY a.CreatedOn " +
                        $"OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY;";
                }


                var apprenticeships = new Dictionary<long, ApprenticeshipEvent>();

                await c.QueryAsync<ApprenticeshipEvent, PriceHistory, ApprenticeshipEvent>(sql, (apprenticeship, history) =>
                {
                    ApprenticeshipEvent existing;
                    if (!apprenticeships.TryGetValue(apprenticeship.Id, out existing))
                    {
                        apprenticeships.Add(apprenticeship.Id, apprenticeship);
                        existing = apprenticeship;
                        existing.PriceHistory = new List<PriceHistory>();
                    }
                    existing.PriceHistory.Add(history);

                    return existing;

                }, parameters);

                return apprenticeships.Values.ToList();

            });


        }

        private DataTable BuildApprenticeshipEventsDataTable(IEnumerable<ApprenticeshipEvent> apprenticeshipEvents)
        {
            var apprenticeshipEventsTable = CreateApprenticeshipsDataTable();

            foreach (var apprenticeshipEvent in apprenticeshipEvents)
            {
                AddApprenticeshipEventToTable(apprenticeshipEventsTable, apprenticeshipEvent);
            }

            return apprenticeshipEventsTable;
        }

        private static DataTable CreateApprenticeshipsDataTable()
        {
            var apprenticeshipEventsTable = new DataTable();

            apprenticeshipEventsTable.Columns.Add("Event", typeof(string));
            apprenticeshipEventsTable.Columns.Add("CreatedOn", typeof(DateTime));
            apprenticeshipEventsTable.Columns.Add("ApprenticeshipId", typeof(long));
            apprenticeshipEventsTable.Columns.Add("PaymentOrder", typeof(int));
            apprenticeshipEventsTable.Columns.Add("PaymentStatus", typeof(short));
            apprenticeshipEventsTable.Columns.Add("AgreementStatus", typeof(short));
            apprenticeshipEventsTable.Columns.Add("ProviderId", typeof(string));
            apprenticeshipEventsTable.Columns.Add("LearnerId", typeof(string));
            apprenticeshipEventsTable.Columns.Add("EmployerAccountId", typeof(string));
            apprenticeshipEventsTable.Columns.Add("TrainingType", typeof(int));
            apprenticeshipEventsTable.Columns.Add("TrainingId", typeof(string));
            apprenticeshipEventsTable.Columns.Add("TrainingStartDate", typeof(DateTime));
            apprenticeshipEventsTable.Columns.Add("TrainingEndDate", typeof(DateTime));
            apprenticeshipEventsTable.Columns.Add("TrainingTotalCost", typeof(decimal));
            apprenticeshipEventsTable.Columns.Add("LegalEntityId", typeof(string));
            apprenticeshipEventsTable.Columns.Add("LegalEntityName", typeof(string));
            apprenticeshipEventsTable.Columns.Add("LegalEntityOrganisationType", typeof(string));
            apprenticeshipEventsTable.Columns.Add("EffectiveFrom", typeof(DateTime));
            apprenticeshipEventsTable.Columns.Add("EffectiveTo", typeof(DateTime));
            apprenticeshipEventsTable.Columns.Add("DateOfBirth", typeof(DateTime));
            return apprenticeshipEventsTable;
        }

        private static DataRow AddApprenticeshipEventToTable(DataTable apprenticeshipEventsTable, ApprenticeshipEvent apprenticeshipEvent)
        {
            var a = apprenticeshipEvent;

            return apprenticeshipEventsTable.Rows.Add(a.Event, a.CreatedOn, a.ApprenticeshipId, a.PaymentOrder, a.PaymentStatus,
                a.AgreementStatus, a.ProviderId, a.LearnerId, a.EmployerAccountId, a.TrainingType, a.TrainingId, a.TrainingStartDate,
                a.TrainingEndDate, a.TrainingTotalCost, a.LegalEntityId, a.LegalEntityName, a.LegalEntityOrganisationType,
                a.EffectiveFrom, a.EffectiveTo, a.DateOfBirth);
        }
    }
}
