using System;
using FluentValidation;

namespace SFA.DAS.Events.Application.Commands.CreateApprenticeshipEvent
{
    public class CreateApprenticeshipEventCommandValidator : AbstractValidator<CreateApprenticeshipEventCommand>
    {
        public CreateApprenticeshipEventCommandValidator()
        {
            RuleFor(model => model.Event).NotEmpty();
        }
    }
}
