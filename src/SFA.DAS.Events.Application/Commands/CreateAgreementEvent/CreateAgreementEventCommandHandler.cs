using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Logging;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Commands.CreateAgreementEvent
{
    public sealed class CreateAgreementEventCommandHandler : AsyncRequestHandler<CreateAgreementEventCommand>
    {
        private readonly IEventsLogger _logger;

        private readonly IAgreementEventRepository _agreementEventRepository;
        private readonly AbstractValidator<CreateAgreementEventCommand> _validator;

        public CreateAgreementEventCommandHandler(IAgreementEventRepository agreementEventRepository, AbstractValidator<CreateAgreementEventCommand> validator, IEventsLogger logger)
        {
            if (agreementEventRepository == null)
                throw new ArgumentNullException(nameof(agreementEventRepository));
            if (validator == null)
                throw new ArgumentNullException(nameof(validator));
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            _agreementEventRepository = agreementEventRepository;
            _validator = validator;
            _logger = logger;
        }

        protected override async Task HandleCore(CreateAgreementEventCommand command)
        {
            _logger.Info($"Received message {command.Event}", accountId: command.EmployerAccountId, providerId: command.ProviderId, @event: command.Event);

            Validate(command);

            try
            {
                var newAgreementEvent = new AgreementEvent
                {
                    Event = command.Event,
                    CreatedOn = DateTime.UtcNow,
                    ProviderId = command.ProviderId,
                    EmployerAccountId = command.EmployerAccountId
                };

                await _agreementEventRepository.Create(newAgreementEvent);

                _logger.Info($"Finished processing message {command.Event}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error processing message {command.Event} - {ex.Message}", accountId: command.EmployerAccountId, providerId: command.ProviderId, @event: command.Event);
                throw;
            }
        }

        private void Validate(CreateAgreementEventCommand command)
        {
            _validator.ValidateAndThrow(command);
        }
    }
}
