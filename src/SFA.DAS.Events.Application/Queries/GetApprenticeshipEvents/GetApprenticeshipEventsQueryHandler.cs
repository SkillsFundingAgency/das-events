using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Queries.GetApprenticeshipEvents
{
    public sealed class GetApprenticeshipEventsQueryHandler : IAsyncRequestHandler<GetApprenticeshipEventsRequest, GetApprenticeshipEventsResponse>
    {
        private readonly IApprenticeshipEventRepository _apprenticeshipEventRepository;

        public GetApprenticeshipEventsQueryHandler(IApprenticeshipEventRepository apprenticeshipEventRepository)
        {
            _apprenticeshipEventRepository = apprenticeshipEventRepository;
        }

        public async Task<GetApprenticeshipEventsResponse> Handle(GetApprenticeshipEventsRequest request)
        {
            Validate(request);

            var events = await _apprenticeshipEventRepository.GetByDateRange(request.FromDateTime, request.ToDateTime, request.PageSize, request.PageNumber);

            return new GetApprenticeshipEventsResponse {Data = MapFrom(events)};
        }

        private static IEnumerable<ApprenticeshipEvent> MapFrom(IEnumerable<ApprenticeshipEvent> source)
        {
            return source; //todo: do mapping to API types
        }

        private void Validate(GetApprenticeshipEventsRequest request)
        {
            var validator = new GetApprenticeshipEventsRequestValidator();

            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}
