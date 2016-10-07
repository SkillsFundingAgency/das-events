using System;
using FluentValidation;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.Commands.CreateApprenticeshipEvent
{
    public class CreateApprenticeshipEventCommandValidator : AbstractValidator<CreateApprenticeshipEventCommand>
    {
        public CreateApprenticeshipEventCommandValidator()
        {
            RuleFor(model => model.Event).NotEmpty();
            RuleFor(model => model.ApprenticeshipId).GreaterThan(0);
            RuleFor(model => model.PaymentStatus).NotEmpty();
            RuleFor(model => model.AgreementStatus).NotEmpty();
            RuleFor(model => model.ProviderId).NotEmpty();
            RuleFor(model => model.LearnerId).NotEmpty();
            RuleFor(model => model.EmployerAccountId).NotEmpty();
            RuleFor(model => model.TrainingType).NotEqual(TrainingTypes.Unspecified);
            RuleFor(model => model.TrainingId).NotEmpty();
            RuleFor(model => model.TrainingStartDate).NotEmpty();
            RuleFor(model => model.TrainingEndDate).NotEmpty();
            RuleFor(start => start.TrainingStartDate).LessThanOrEqualTo(end => end.TrainingEndDate);
            RuleFor(model => model.TrainingTotalCost).GreaterThanOrEqualTo(0);
        }
    }
}
