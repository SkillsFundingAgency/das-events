using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Queries.GetGenericEventsSinceEvent
{
    public class GetGenericEventsSinceEventQueryHandler : IAsyncRequestHandler<GetGenericEventsSinceEventRequest, GetGenericEventsSinceEventResponse>
    {
        private readonly IGenericEventRepository _repository;
        private readonly IValidator<GetGenericEventsSinceEventRequest> _validator;

        public GetGenericEventsSinceEventQueryHandler(IGenericEventRepository repository, IValidator<GetGenericEventsSinceEventRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<GetGenericEventsSinceEventResponse> Handle(GetGenericEventsSinceEventRequest sinceEventRequest)
        {
            _validator.ValidateAndThrow(sinceEventRequest);

            var events = await _repository.GetSinceEvent(
                sinceEventRequest.EventTypes, 
                sinceEventRequest.FromEventId,
                sinceEventRequest.PageSize, 
                sinceEventRequest.PageNumber);

            return new GetGenericEventsSinceEventResponse
            {
                Data = events
            };
        }
    }
}
