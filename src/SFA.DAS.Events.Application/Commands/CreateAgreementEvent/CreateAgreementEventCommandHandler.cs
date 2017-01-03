using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Logging;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Commands.CreateAgreementEvent
{
    public class CreateAgreementEventCommandHandler : AsyncRequestHandler<CreateAgreementEventCommand>
    {
        private readonly IEventsLogger _logger;

        private readonly IAgreementEventRepository _agreementEventRepository;

        public CreateAgreementEventCommandHandler(IAgreementEventRepository agreementEventRepository, IEventsLogger logger)
        {
            if (agreementEventRepository == null)
                throw new ArgumentNullException(nameof(agreementEventRepository));
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            _agreementEventRepository = agreementEventRepository;
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

        private static void Validate(CreateAgreementEventCommand command)
        {
            var validator = new CreateAgreementEventCommandValidator();

            var validationResult = validator.Validate(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}
