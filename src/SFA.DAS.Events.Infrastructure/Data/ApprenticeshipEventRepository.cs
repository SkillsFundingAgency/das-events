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
        protected override string TableName => "ApprenticeshipEvents";

        public ApprenticeshipEventRepository(string databaseConnectionString) : base(databaseConnectionString) {}

        public async Task Create(ApprenticeshipEvent @event)
        {
            await WithConnection(async c =>
                await c.ExecuteAsync($"INSERT INTO [dbo].[{TableName}](Event, CreatedOn, ApprenticeshipId, PaymentStatus, AgreementStatus, ProviderId, LearnerId, EmployerAccountId, TrainingType, TrainingId, TrainingStartDate, TrainingEndDate, TrainingTotalCost, PaymentOrder) " +
                                     $"VALUES (@event, @createdOn, @apprenticeshipId, @paymentStatus, @agreementStatus, @providerId, @learnerId, @employerAccountId, @trainingType, @trainingId, @trainingStartDate, @trainingEndDate, @trainingTotalCost, @paymentOrder);", @event));
        }

        public async Task<IEnumerable<ApprenticeshipEvent>> GetByRange(DateTime fromDate, DateTime toDate, int pageSize, int pageNumber, long fromEventId)
        {
            return await GetByRange<ApprenticeshipEvent>(fromDate, toDate, pageSize, pageNumber, fromEventId);
        }
    }
}
