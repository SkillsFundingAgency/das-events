using System;
using MediatR;

namespace SFA.DAS.Events.Application.Queries.GetApprenticeshipEvents
{
    public sealed class GetApprenticeshipEventsRequest : IAsyncRequest<GetApprenticeshipEventsResponse>
    {
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public long FromEventId { get; set; }
    }
}
