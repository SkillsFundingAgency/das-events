using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Logging;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Commands.BulkUploadCreateApprenticeshipEvents
{
    public sealed class BulkUploadCreateApprenticeshipEventsCommandHandler : AsyncRequestHandler<BulkUploadCreateApprenticeshipEventsCommand>
    {
        private readonly IApprenticeshipEventRepository _apprenticeshipEventRepository;
        private readonly IEventsLogger _logger;
        private readonly AbstractValidator<BulkUploadCreateApprenticeshipEventsCommand> _validator;

        public BulkUploadCreateApprenticeshipEventsCommandHandler(IApprenticeshipEventRepository apprenticeshipEventRepository, IEventsLogger logger, AbstractValidator<BulkUploadCreateApprenticeshipEventsCommand> validator)
        {
            if (apprenticeshipEventRepository == null)
                throw new ArgumentNullException(nameof(apprenticeshipEventRepository));
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));
            if (validator == null)
                throw new ArgumentNullException(nameof(validator));

            _apprenticeshipEventRepository = apprenticeshipEventRepository;
            _logger = logger;
            _validator = validator;
        }

        protected override async Task HandleCore(BulkUploadCreateApprenticeshipEventsCommand command)
        {
            Validate(command);

            SetCreatedDate(command.ApprenticeshipEvents);

            await _apprenticeshipEventRepository.BulkUploadApprenticeshipEvents(command.ApprenticeshipEvents);
        }

        private static void SetCreatedDate(IEnumerable<ApprenticeshipEvent> events)
        {
            foreach (var @event in events)
            {
                @event.CreatedOn = DateTime.UtcNow;
            }
        }

        private void Validate(BulkUploadCreateApprenticeshipEventsCommand command)
        {
            _validator.ValidateAndThrow(command);
        }
    }
}
