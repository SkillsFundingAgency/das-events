using FluentValidation;
using SFA.DAS.Events.Application.Validation;

namespace SFA.DAS.Events.Application.Commands.BulkUploadCreateApprenticeshipEvents
{
    public sealed class BulkUploadCreateApprenticeshipEventsCommandValidator : AbstractValidator<BulkUploadCreateApprenticeshipEventsCommand>
    {
        public BulkUploadCreateApprenticeshipEventsCommandValidator()
        {
            RuleFor(x => x.ApprenticeshipEvents).NotEmpty().SetCollectionValidator(new ApprenticeshipEventValidator());
        }
    }
}
