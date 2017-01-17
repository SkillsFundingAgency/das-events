using SFA.DAS.Events.Application.Queries.GetApprenticeshipEvents;

namespace SFA.DAS.Events.Application.UnitTests.Builders
{
    internal class GetApprenticeshipEventsRequestBuilder : GetEventRequestBuilder<GetApprenticeshipEventsRequest>
    {
        internal override GetApprenticeshipEventsRequest Build()
        {
            return new GetApprenticeshipEventsRequest
            {
                FromDateTime = FromDateTime,
                FromEventId = FromEventId,
                PageNumber = PageNumber,
                PageSize = PageSize,
                ToDateTime = ToDateTime
            };
        }
    }
}
