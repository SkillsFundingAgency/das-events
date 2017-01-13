using System;
using MediatR;

namespace SFA.DAS.Events.Application.Queries
{
    public abstract class QueryRequest<TResponse> : IAsyncRequest<TResponse>
    {
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public long FromEventId { get; set; }
    }
}
