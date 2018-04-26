using FluentValidation;
using SFA.DAS.Events.Application.Commands.CreateApprenticeshipEvent;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.Commands.BulkUploadCreateApprenticeshipEvents
{
    public class BulkUploadCreateApprentieshipEventsCommandValidator : AbstractValidator<BulkUploadCreateApprenticeshipEventsCommand>
    {
        public BulkUploadCreateApprentieshipEventsCommandValidator()
        {
            RuleFor(x => x.ApprenticeshipEvents).SetCollectionValidator(new ApprenticeshipEventValidator());           
        }

        public class ApprenticeshipEventValidator : AbstractValidator<ApprenticeshipEvent>
        {
            public ApprenticeshipEventValidator()
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
}
