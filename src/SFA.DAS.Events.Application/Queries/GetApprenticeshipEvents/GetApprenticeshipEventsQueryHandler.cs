using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Queries.GetApprenticeshipEvents
{
    public sealed class GetApprenticeshipEventsQueryHandler : IAsyncRequestHandler<GetApprenticeshipEventsRequest, GetApprenticeshipEventsResponse>
    {
        private readonly IApprenticeshipEventRepository _apprenticeshipEventRepository;
        private readonly AbstractValidator<GetApprenticeshipEventsRequest> _validator;

        public GetApprenticeshipEventsQueryHandler(IApprenticeshipEventRepository apprenticeshipEventRepository, AbstractValidator<GetApprenticeshipEventsRequest> validator)
        {
            if (apprenticeshipEventRepository == null)
                throw new ArgumentNullException(nameof(apprenticeshipEventRepository));
            if (validator == null)
                throw new ArgumentNullException(nameof(validator));

            _apprenticeshipEventRepository = apprenticeshipEventRepository;
            _validator = validator;
        }

        public async Task<GetApprenticeshipEventsResponse> Handle(GetApprenticeshipEventsRequest request)
        {
            Validate(request);

            var events = await _apprenticeshipEventRepository.GetByRange(request.FromDateTime, request.ToDateTime, request.PageSize, request.PageNumber, request.FromEventId);

            return new GetApprenticeshipEventsResponse {Data = events};
        }

        private void Validate(GetApprenticeshipEventsRequest request)
        {
            _validator.ValidateAndThrow(request);
        }
    }
}
