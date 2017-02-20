using System.Collections.Generic;

namespace SFA.DAS.Events.Application.Queries.GetGenericEvents
{
    public class GetGenericEventsRequest : QueryRequest<GetGenericEventsResponse>
    {
        public IEnumerable<string> EventTypes { get; set; }
    }
}
