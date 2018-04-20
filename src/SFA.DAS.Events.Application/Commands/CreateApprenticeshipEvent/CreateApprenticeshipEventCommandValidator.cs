using FluentValidation;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.Commands.CreateApprenticeshipEvent
{
    public class CreateApprenticeshipEventCommandValidator : AbstractValidator<CreateApprenticeshipEventCommand>
    {
        public CreateApprenticeshipEventCommandValidator()
        {
            RuleFor(x => x.TransferSenderName)
                .NotEmpty()
                .When(x => x.TransferSenderId.HasValue && x.TransferSenderId > 0)
                .WithMessage("'Transfer Sender Name' should not be empty if 'Transfer Sender Id' is specified");

            RuleFor(x => x.TransferSenderId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .When(x => !string.IsNullOrWhiteSpace(x.TransferSenderName))
                .WithMessage("'Transfer Sender Id' should not be empty if 'Transfer Sender Name' is specified");

            RuleFor(x => x.TransferSenderId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .When(x => x.TransferApprovalStatus.HasValue)
                .WithMessage("'Transfer Sender Id' should not be empty if 'Transfer Approval Status' is set");

        }
    }
}
