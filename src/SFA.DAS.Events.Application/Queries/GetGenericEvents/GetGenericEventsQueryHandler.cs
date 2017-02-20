using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Queries.GetGenericEvents
{
    public class GetGenericEventsQueryHandler : IAsyncRequestHandler<GetGenericEventsRequest, GetGenericEventsResponse>
    {
        private readonly IGenericEventRepository _repository;
        private readonly IValidator<GetGenericEventsRequest> _validator;

        public GetGenericEventsQueryHandler(IGenericEventRepository repository, IValidator<GetGenericEventsRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<GetGenericEventsResponse> Handle(GetGenericEventsRequest request)
        {
            _validator.ValidateAndThrow(request);

            var events = await _repository.GetByDateRange(
                request.EventTypes, 
                request.FromDateTime, 
                request.ToDateTime, 
                request.PageSize, 
                request.PageNumber);

            return new GetGenericEventsResponse
            {
                Data = events
            };
        }
    }
}
