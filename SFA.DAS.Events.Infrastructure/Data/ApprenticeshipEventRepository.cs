using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Repositories;
using SFA.DAS.Events.Infrastructure.Configuration;

namespace SFA.DAS.Events.Infrastructure.Data
{
    public class ApprenticeshipEventRepository : BaseEventRepository, IApprenticeshipEventRepository
    {
        private const string TableName = "ApprenticeshipEvents";

        public ApprenticeshipEventRepository(string databaseConnectionString) : base(databaseConnectionString) {}

        public async Task Create(ApprenticeshipEvent @event)
        {
            await Create(TableName, @event);
        }

        public async Task<IEnumerable<ApprenticeshipEvent>> GetByDateRange(DateTime @from, DateTime to)
        {
            return await GetByDateRange<ApprenticeshipEvent>(TableName, @from, to);
        }
    }
}
