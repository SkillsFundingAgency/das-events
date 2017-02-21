using System.Collections.Generic;

namespace SFA.DAS.Events.Application.Queries.GetGenericEventsSinceEvent
{
    public class GetGenericEventsSinceEventRequest : QueryRequest<GetGenericEventsSinceEventResponse>
    {
        public IEnumerable<string> EventTypes { get; set; }
    }
}
