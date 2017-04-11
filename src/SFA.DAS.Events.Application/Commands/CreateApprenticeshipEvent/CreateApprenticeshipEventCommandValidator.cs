using FluentValidation;

namespace SFA.DAS.Events.Application.Commands.CreateApprenticeshipEvent
{
    public class CreateApprenticeshipEventCommandValidator : AbstractValidator<CreateApprenticeshipEventCommand>
    {
        public CreateApprenticeshipEventCommandValidator()
        {
            RuleFor(model => model.Event).NotEmpty();
            RuleFor(model => model.ApprenticeshipId).GreaterThan(0);
            RuleFor(model => model.LearnerId).NotEmpty();
            RuleFor(model => model.EmployerAccountId).NotEmpty();
            RuleFor(model => model.TrainingType).IsInEnum();
            RuleFor(model => model.TrainingStartDate).NotEmpty();
            RuleFor(model => model.TrainingEndDate).NotEmpty();
            RuleFor(start => start.TrainingStartDate).LessThanOrEqualTo(end => end.TrainingEndDate);
            /* RuleFor(model => model.LegalEntityId).NotEmpty();
            RuleFor(model => model.LegalEntityName).NotEmpty();
            RuleFor(model => model.LegalEntityOrganisationType).NotEmpty(); */
        }
    }
}
