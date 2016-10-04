using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using NLog;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Commands.CreateApprenticeshipEvent
{
    public class CreateApprenticeshipEventCommandHandler : AsyncRequestHandler<CreateApprenticeshipEventCommand>
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IApprenticeshipEventRepository _apprenticeshipEventRepository;

        public CreateApprenticeshipEventCommandHandler(IApprenticeshipEventRepository apprenticeshipEventRepository)
        {
            _apprenticeshipEventRepository = apprenticeshipEventRepository;
        }

        protected override async Task HandleCore(CreateApprenticeshipEventCommand message)
        {
            Logger.Info($"Received message {message.Event}");

            Validate(message);

            try
            {
                //todo: add serialized apprenticeship data here?
                var data = string.Empty;

                var newApprenticeshipEvent = new ApprenticeshipEvent
                {
                    Event = message.Event,
                    EventType = EventTypes.Apprenticeships,
                    CreatedOn = DateTime.UtcNow,
                    Data = data
                };

                await _apprenticeshipEventRepository.Create(newApprenticeshipEvent);

                Logger.Info($"Finished processing message {message.Event}");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error processing message {message.Event} - {ex.Message}");
            }
        }

        private void Validate(CreateApprenticeshipEventCommand command)
        {
            var validator = new CreateApprenticeshipEventCommandValidator();

            var validationResult = validator.Validate(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}
