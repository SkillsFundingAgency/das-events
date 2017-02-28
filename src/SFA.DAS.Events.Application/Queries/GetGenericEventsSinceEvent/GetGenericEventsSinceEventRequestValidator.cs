using FluentValidation;

namespace SFA.DAS.Events.Application.Queries.GetGenericEventsSinceEvent
{
    public class GetGenericEventsSinceEventRequestValidator : AbstractValidator<GetGenericEventsSinceEventRequest>
    {
        public GetGenericEventsSinceEventRequestValidator()
        {
            RuleFor(request => request.EventTypes);
            RuleForEach(request => request.EventTypes).NotEmpty();
            RuleFor(request => request.FromEventId).GreaterThanOrEqualTo(0);
            RuleFor(request => request.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(request => request.PageSize).InclusiveBetween(1, 10000); //todo: determine appropriate max page size
        }
    }
}
