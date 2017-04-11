using System;
using System.Collections.Generic;
using System.Data;
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
            await WithConnection(async c =>
                await c.ExecuteAsync($"INSERT INTO [dbo].[{TableName}](Event, CreatedOn, ApprenticeshipId, PaymentStatus, AgreementStatus, ProviderId, LearnerId, EmployerAccountId, TrainingType, TrainingId, TrainingStartDate, TrainingEndDate, TrainingTotalCost, PaymentOrder, LegalEntityId, LegalEntityName, LegalEntityOrganisationType, EffectiveFrom, EffectiveTo) " +
                                     $"VALUES (@event, @createdOn, @apprenticeshipId, @paymentStatus, @agreementStatus, @providerId, @learnerId, @employerAccountId, @trainingType, @trainingId, @trainingStartDate, @trainingEndDate, @trainingTotalCost, @paymentOrder, @legalEntityId, @legalEntityName, @legalEntityOrganisationType, @effectiveFrom, @effectiveTo);", @event));
        }

        public async Task BulkUploadApprenticeshipEvents(IList<ApprenticeshipEvent> apprenticeshipEvents)
        {
            _logger.Debug($"Bulk uploading {apprenticeshipEvents.Count} apprenticeship events");

            var table = BuildApprenticeshipEventsDataTable(apprenticeshipEvents);

            var insertedApprenticeships = await WithConnection(x =>
            {
                return x.ExecuteAsync(
                    commandType: CommandType.StoredProcedure,
                    sql: "[dbo].[BulkUploadApprenticeshipEvents]",
                    param: new { @apprenticeshipEvents = table.AsTableValuedParameter("dbo.ApprenticeshipEventsTable") });
            });
        }

        public async Task<IEnumerable<ApprenticeshipEvent>> GetByRange(DateTime fromDate, DateTime toDate, int pageSize, int pageNumber, long fromEventId)
        {
            return await GetByRange<ApprenticeshipEvent>(fromDate, toDate, pageSize, pageNumber, fromEventId);
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
            return apprenticeshipEventsTable;
        }

        private static DataRow AddApprenticeshipEventToTable(DataTable apprenticeshipEventsTable, ApprenticeshipEvent apprenticeshipEvent)
        {
            var a = apprenticeshipEvent;

            return apprenticeshipEventsTable.Rows.Add(a.Event, a.CreatedOn, a.ApprenticeshipId, a.PaymentOrder, a.PaymentStatus,
                a.AgreementStatus, a.ProviderId, a.LearnerId, a.EmployerAccountId, a.TrainingType, a.TrainingId, a.TrainingStartDate,
                a.TrainingEndDate, a.TrainingTotalCost, a.LegalEntityId, a.LegalEntityName, a.LegalEntityOrganisationType,
                a.EffectiveFrom, a.EffectiveTo);
        }
    }
}
