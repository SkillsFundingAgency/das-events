using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Queries.GetGenericEventsByDateRange
{
    public class GetGenericEventsByDateRangeQueryHandler : IAsyncRequestHandler<GetGenericEventsByDateRangeRequest, GetGenericEventsByDateRangeResponse>
    {
        private readonly IGenericEventRepository _repository;
        private readonly IValidator<GetGenericEventsByDateRangeRequest> _validator;

        public GetGenericEventsByDateRangeQueryHandler(IGenericEventRepository repository, IValidator<GetGenericEventsByDateRangeRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<GetGenericEventsByDateRangeResponse> Handle(GetGenericEventsByDateRangeRequest request)
        {
            _validator.ValidateAndThrow(request);

            var events = await _repository.GetByDateRange(
                request.EventTypes, 
                request.FromDateTime, 
                request.ToDateTime, 
                request.PageSize, 
                request.PageNumber);

            return new GetGenericEventsByDateRangeResponse
            {
                Data = events
            };
        }
    }
}
