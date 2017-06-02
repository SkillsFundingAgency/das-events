using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Queries.GetGenericEventsByResourceUri
{
    public class GetGenericEventsByResourceUriQueryHandler : IAsyncRequestHandler<GetGenericEventsByResourceUriRequest, GetGenericEventsByResourceUriResponse>
    {
        private readonly IGenericEventRepository _repository;

        public GetGenericEventsByResourceUriQueryHandler(IGenericEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetGenericEventsByResourceUriResponse> Handle(GetGenericEventsByResourceUriRequest request)
        {
            var events = await _repository.GetByResourceUri(
                request.ResourceUri, 
                request.FromDateTime, 
                request.ToDateTime, 
                request.PageSize, 
                request.PageNumber);

            return new GetGenericEventsByResourceUriResponse
            {
                Data = events
            };
        }
    }
}
