using System;
using FluentValidation;

namespace SFA.DAS.Events.Application.Queries.GetApprenticeshipEvents
{
    public class GetApprenticeshipEventsRequestValidator : AbstractValidator<GetApprenticeshipEventsRequest>
    {
        public GetApprenticeshipEventsRequestValidator()
        {
            RuleFor(from => from.FromDateTime).LessThanOrEqualTo(to => to.ToDateTime);
            RuleFor(request => request.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(request => request.PageSize).InclusiveBetween(1, 10000); //todo: determine appropriate max page size
            RuleFor(request => request.FromEventId).GreaterThanOrEqualTo(0);
        }
    }
}
