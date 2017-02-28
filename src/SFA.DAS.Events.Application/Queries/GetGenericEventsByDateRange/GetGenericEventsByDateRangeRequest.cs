using System.Collections.Generic;

namespace SFA.DAS.Events.Application.Queries.GetGenericEventsByDateRange
{
    public class GetGenericEventsByDateRangeRequest : QueryRequest<GetGenericEventsByDateRangeResponse>
    {
        public IEnumerable<string> EventTypes { get; set; }
    }
}
