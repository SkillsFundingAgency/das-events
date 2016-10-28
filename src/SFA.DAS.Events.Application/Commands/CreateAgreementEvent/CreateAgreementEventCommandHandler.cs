using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using NLog;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Commands.CreateAgreementEvent
{
    public class CreateAgreementEventCommandHandler : AsyncRequestHandler<CreateAgreementEventCommand>
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IAgreementEventRepository _agreementEventRepository;

        public CreateAgreementEventCommandHandler(IAgreementEventRepository agreementEventRepository)
        {
            _agreementEventRepository = agreementEventRepository;
        }

        protected override async Task HandleCore(CreateAgreementEventCommand command)
        {
            Logger.Info($"Received message {command.Event}");

            Validate(command);

            try
            {
                var newAgreementEvent = new AgreementEvent
                {
                    Event = command.Event,
                    CreatedOn = DateTime.UtcNow,
                    ProviderId = command.ProviderId
                };

                await _agreementEventRepository.Create(newAgreementEvent);

                Logger.Info($"Finished processing message {command.Event}");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error processing message {command.Event} - {ex.Message}");
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
