using FluentValidation;

namespace SFA.DAS.Events.Application.Commands.CreateAccountEvent
{
    public class CreateAccountEventCommandValidator : AbstractValidator<CreateAccountEventCommand>
    {
        public CreateAccountEventCommandValidator()
        {
            RuleFor(model => model.Event).NotEmpty();
            RuleFor(model => model.EmployerAccountId).NotEmpty();
        }
    }
}
