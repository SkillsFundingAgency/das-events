using FluentValidation;

namespace SFA.DAS.Events.Application.Commands.CreateGenericEvent
{
    public class CreateGenericEventCommandValidator : AbstractValidator<CreateGenericEventCommand>
    {
        public CreateGenericEventCommandValidator()
        {
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Payload).NotEmpty();
        }
    }
}
