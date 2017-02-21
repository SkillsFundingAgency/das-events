using FluentValidation;

namespace SFA.DAS.Events.Application.Queries.GetGenericEventsByDateRange
{
    public class GetGenericEventsByDateRangeRequestValidator : AbstractValidator<GetGenericEventsByDateRangeRequest>
    {
        public GetGenericEventsByDateRangeRequestValidator()
        {
            RuleFor(request => request.EventTypes);
            RuleForEach(request => request.EventTypes).NotEmpty();
            RuleFor(from => from.FromDateTime).LessThanOrEqualTo(to => to.ToDateTime);
            RuleFor(request => request.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(request => request.PageSize).InclusiveBetween(1, 10000); //todo: determine appropriate max page size
        }
    }
}
