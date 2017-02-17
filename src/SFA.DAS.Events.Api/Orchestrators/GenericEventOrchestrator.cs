using System.Collections.Generic;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Api.Orchestrators
{
    public class GenericEventOrchestrator : IGenericEventOrchestrator
    {
        public ICollection<GenericEvent> GetEvents(string eventType, string fromDate, string toDate, int pageSize, int pageNumber, long fromEventId)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<GenericEvent> GetEvents(IEnumerable<string> eventTypes, string fromDate, string toDate, int pageSize, int pageNumber,
            long fromEventId)
        {
            throw new System.NotImplementedException();
        }
    }
}