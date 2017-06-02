using System;
using MediatR;

namespace SFA.DAS.Events.Application.Queries.GetGenericEventsByResourceId
{
    public class GetGenericEventsByResourceIdRequest : IAsyncRequest<GetGenericEventsByResourceIdResponse>
    {
        public string ResourceType { get; set; }
        public string ResourceId { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
