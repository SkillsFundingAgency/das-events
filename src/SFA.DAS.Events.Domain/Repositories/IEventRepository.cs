using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Domain.Repositories
{
    public interface IEventRepository<T> where T : BaseEvent
    {
        Task Create(T @event);

        Task<IEnumerable<T>> GetByDateRange(DateTime @from, DateTime to);
    }
}
