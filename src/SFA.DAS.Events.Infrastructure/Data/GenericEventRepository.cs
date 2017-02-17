using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Infrastructure.Data
{
    public class GenericEventRepository : IEventRepository<GenericEvent>
    {
        public Task Create(GenericEvent @event)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GenericEvent>> GetByRange(DateTime fromDate, DateTime toDate, int pageSize, int pageNumber, long fromEventId)
        {
            throw new NotImplementedException();
        }
    }
}
