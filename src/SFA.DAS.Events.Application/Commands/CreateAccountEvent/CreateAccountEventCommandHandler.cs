using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Logging;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Commands.CreateAccountEvent
{
    public class CreateAccountEventCommandHandler : AsyncRequestHandler<CreateAccountEventCommand>
    {
        private readonly IEventsLogger _logger;

        private readonly IAccountEventRepository _accountEventRepository;

        public CreateAccountEventCommandHandler(IAccountEventRepository accountEventRepository, IEventsLogger logger)
        {
            if (accountEventRepository == null)
                throw new ArgumentNullException(nameof(accountEventRepository));
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            _accountEventRepository = accountEventRepository;
            _logger = logger;
        }

        protected override async Task HandleCore(CreateAccountEventCommand command)
        {
            _logger.Info($"Received message {command.Event}", accountId: command.EmployerAccountId, @event: command.Event);

            Validate(command);

            try
            {
                var newAccountEvent = new AccountEvent
                {
                    Event = command.Event,
                    CreatedOn = DateTime.UtcNow,
                    EmployerAccountId = command.EmployerAccountId
                };

                await _accountEventRepository.Create(newAccountEvent);

                _logger.Info($"Finished processing message {command.Event}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error processing message {command.Event} - {ex.Message}", accountId: command.EmployerAccountId, @event: command.Event);
                throw;
            }
        }

        private static void Validate(CreateAccountEventCommand command)
        {
            var validator = new CreateAccountEventCommandValidator();

            var validationResult = validator.Validate(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}
