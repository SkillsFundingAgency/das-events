using SFA.DAS.Events.Application.Queries.GetAgreementEvents;
using SFA.DAS.Events.Application.Queries.GetApprenticeshipEvents;

namespace SFA.DAS.Events.Application.UnitTests.Builders
{
    public class GetApprenticeshipEventsRequestBuilder : GetEventRequestBuilder<GetApprenticeshipEventsRequest>
    {
        public override GetApprenticeshipEventsRequest Build()
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
