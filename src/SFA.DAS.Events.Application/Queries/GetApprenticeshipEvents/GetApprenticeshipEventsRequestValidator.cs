using System;
using FluentValidation;

namespace SFA.DAS.Events.Application.Queries.GetApprenticeshipEvents
{
    public class GetApprenticeshipEventsRequestValidator : AbstractValidator<GetApprenticeshipEventsRequest>
    {
        public GetApprenticeshipEventsRequestValidator()
        {
            RuleFor(from => from.FromDateTime).LessThanOrEqualTo(to => to.ToDateTime);
        }
    }
}
