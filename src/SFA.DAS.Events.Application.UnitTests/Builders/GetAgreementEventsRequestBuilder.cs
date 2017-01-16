using SFA.DAS.Events.Application.Queries.GetAgreementEvents;

namespace SFA.DAS.Events.Application.UnitTests.Builders
{
    public class GetAgreementEventsRequestBuilder : GetEventRequestBuilder<GetAgreementEventsRequest>
    {
        public override GetAgreementEventsRequest Build()
        {
            return new GetAgreementEventsRequest
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
