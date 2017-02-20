using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Api.Orchestrators
{
    public interface IGenericEventOrchestrator
    {
        Task<ICollection<GenericEvent>> GetEvents(
          string eventType, DateTime fromDate, DateTime toDate, int pageSize, int pageNumber, long fromEventId);

        Task<ICollection<GenericEvent>> GetEvents(
            IEnumerable<string> eventTypes, DateTime fromDate, DateTime toDate, int pageSize, int pageNumber, long fromEventId);
    }
}