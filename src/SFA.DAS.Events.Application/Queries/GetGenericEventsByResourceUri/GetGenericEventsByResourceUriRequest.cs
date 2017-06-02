using System;
using MediatR;

namespace SFA.DAS.Events.Application.Queries.GetGenericEventsByResourceUri
{
    public class GetGenericEventsByResourceUriRequest : IAsyncRequest<GetGenericEventsByResourceUriResponse>
    {
        public string ResourceUri { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
