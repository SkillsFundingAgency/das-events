using FluentValidation;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.Validation
{
    public sealed class ApprenticeshipEventValidator : AbstractValidator<ApprenticeshipEvent>
    {
        public ApprenticeshipEventValidator()
        {
            RuleFor(model => model.Event).NotEmpty();
            RuleFor(model => model.ApprenticeshipId).GreaterThan(0);
            RuleFor(model => model.EmployerAccountId).NotEmpty();
            RuleFor(model => model.TrainingType).IsInEnum();
        }
    }
}
