using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Queries.GetGenericEventsByResourceId
{
    public class GetGenericEventsByResourceIdQueryHandler : IAsyncRequestHandler<GetGenericEventsByResourceIdRequest, GetGenericEventsByResourceIdResponse>
    {
        private readonly IGenericEventRepository _repository;

        public GetGenericEventsByResourceIdQueryHandler(IGenericEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetGenericEventsByResourceIdResponse> Handle(GetGenericEventsByResourceIdRequest request)
        {
            var events = await _repository.GetByResourceId(
                request.ResourceType, 
                request.ResourceId,
                request.FromDateTime, 
                request.ToDateTime, 
                request.PageSize, 
                request.PageNumber);

            return new GetGenericEventsByResourceIdResponse
            {
                Data = events
            };
        }
    }
}
