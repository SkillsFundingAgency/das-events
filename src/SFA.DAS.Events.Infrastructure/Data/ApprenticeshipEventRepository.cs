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
                await c.ExecuteAsync($"INSERT INTO [dbo].[{TableName}](Event, CreatedOn, PaymentStatus, AgreementStatus, ProviderId, LearnerId, EmployerAccountId, TrainingType, TrainingId, TrainingStartDate, TrainingEndDate, TrainingTotalCost) VALUES (@event, @createdOn, @paymentStatus, @agreementStatus, @providerId, @learnerId, @employerAccountId, @trainingType, @trainingId, @trainingStartDate, @trainingEndDate, @trainingTotalCost);", @event));
        }

        public async Task<IEnumerable<ApprenticeshipEvent>> GetByDateRange(DateTime @from, DateTime to)
        {
            return await WithConnection(async c =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@from", from);
                parameters.Add("@to", to);

                var results = await c.QueryAsync<ApprenticeshipEvent>($"SELECT * FROM [dbo].[{TableName}] WHERE CreatedOn >=  @from AND CreatedOn < @to;", parameters);

                return results;
            });
        }
    }
}
