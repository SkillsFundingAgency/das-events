using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Logging;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Commands.CreateAccountEvent
{
    public sealed class CreateAccountEventCommandHandler : AsyncRequestHandler<CreateAccountEventCommand>
    {
        private readonly IEventsLogger _logger;

        private readonly IAccountEventRepository _accountEventRepository;
        private readonly AbstractValidator<CreateAccountEventCommand> _validator;

        public CreateAccountEventCommandHandler(IAccountEventRepository accountEventRepository, AbstractValidator<CreateAccountEventCommand> validator, IEventsLogger logger)
        {
            if (accountEventRepository == null)
                throw new ArgumentNullException(nameof(accountEventRepository));
            if (validator == null)
                throw new ArgumentNullException(nameof(validator));
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            _accountEventRepository = accountEventRepository;
            _validator = validator;
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

        private void Validate(CreateAccountEventCommand command)
        {
            _validator.ValidateAndThrow(command);
        }
    }
}
