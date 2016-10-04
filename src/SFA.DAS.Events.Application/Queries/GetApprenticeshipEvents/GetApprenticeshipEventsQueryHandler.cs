using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<GetApprenticeshipEventsResponse> Handle(GetApprenticeshipEventsRequest message)
        {
            var events = await _apprenticeshipEventRepository.GetByDateRange(message.FromDateTime, message.ToDateTime);

            return new GetApprenticeshipEventsResponse {Data = MapFrom(events)};
        }

        private static IEnumerable<ApprenticeshipEvent> MapFrom(IEnumerable<ApprenticeshipEvent> source)
        {
            return source; //todo: do mapping to API types
        }
    }
}
