using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Api.Orchestrators
{
    public interface IGenericEventOrchestrator
    {
        Task CreateEvent(GenericEvent @event);
        Task<ICollection<GenericEvent>> GetEventsByDateRange(
            IEnumerable<string> eventTypes, DateTime fromDate, DateTime toDate, int pageSize, int pageNumber);

        Task<ICollection<GenericEvent>> GetEventsSinceEvent(
            IEnumerable<string> eventTypes, long fromEventId, int pageSize, int pageNumber);
    }
}