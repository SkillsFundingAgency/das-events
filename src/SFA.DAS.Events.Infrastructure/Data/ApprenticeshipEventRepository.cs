using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Infrastructure.Data
{
    public class ApprenticeshipEventRepository : BaseRepository, IApprenticeshipEventRepository
    {
        private const string TableName = "ApprenticeshipEvents";

        public ApprenticeshipEventRepository(string databaseConnectionString) : base(databaseConnectionString) {}

        public async Task Create(ApprenticeshipEvent @event)
        {
            await WithConnection(async c =>
                await c.ExecuteAsync($"INSERT INTO [dbo].[{TableName}](Event, CreatedOn, ApprenticeshipId, PaymentStatus, AgreementStatus, ProviderId, LearnerId, EmployerAccountId, TrainingType, TrainingId, TrainingStartDate, TrainingEndDate, TrainingTotalCost, PaymentOrder) " + 
                $"VALUES (@event, @createdOn, @apprenticeshipId, @paymentStatus, @agreementStatus, @providerId, @learnerId, @employerAccountId, @trainingType, @trainingId, @trainingStartDate, @trainingEndDate, @trainingTotalCost, @paymentOrder);", @event));
        }

        public async Task<IEnumerable<ApprenticeshipEvent>> GetByDateRange(DateTime fromDate, DateTime toDate, int pageSize, int pageNumber, long fromEventId)
        {
            var offset = pageSize*(pageNumber - 1);

            // query by ID or by date range
            if (fromEventId > 0)
            {
                return await WithConnection(async c =>
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@fromEventId", fromEventId);

                    var results = await c.QueryAsync<ApprenticeshipEvent>($"SELECT * FROM [dbo].[{TableName}] WHERE Id >=  @fromEventId ORDER BY CreatedOn OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY;", parameters);

                    return results;
                });
            }
            else
            {
                return await WithConnection(async c =>
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@fromDate", fromDate);
                    parameters.Add("@toDate", toDate);

                    var results = await c.QueryAsync<ApprenticeshipEvent>($"SELECT * FROM [dbo].[{TableName}] WHERE CreatedOn >=  @fromDate AND CreatedOn < @toDate ORDER BY CreatedOn OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY;", parameters);

                    return results;
                });
            }
        }
    }
}
