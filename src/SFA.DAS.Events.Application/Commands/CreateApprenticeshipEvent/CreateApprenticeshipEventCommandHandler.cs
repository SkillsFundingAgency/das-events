using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Logging;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Commands.CreateApprenticeshipEvent
{
    public class CreateApprenticeshipEventCommandHandler : AsyncRequestHandler<CreateApprenticeshipEventCommand>
    {
        private readonly IEventsLogger _logger;
        private readonly IApprenticeshipEventRepository _apprenticeshipEventRepository;

        public CreateApprenticeshipEventCommandHandler(IApprenticeshipEventRepository apprenticeshipEventRepository, IEventsLogger logger)
        {
            if (apprenticeshipEventRepository == null)
                throw new ArgumentNullException(nameof(apprenticeshipEventRepository));
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            _apprenticeshipEventRepository = apprenticeshipEventRepository;
            _logger = logger;
        }

        protected override async Task HandleCore(CreateApprenticeshipEventCommand command)
        {
            _logger.Info($"Received message {command.Event}", accountId: command.EmployerAccountId, providerId: command.ProviderId, @event: command.Event);

            Validate(command);

            try
            {
                var newApprenticeshipEvent = new ApprenticeshipEvent
                {
                    Event = command.Event,
                    CreatedOn = DateTime.UtcNow,
                    ApprenticeshipId = command.ApprenticeshipId,
                    PaymentStatus = command.PaymentStatus,
                    AgreementStatus = command.AgreementStatus,
                    ProviderId = command.ProviderId,
                    LearnerId = command.LearnerId,
                    EmployerAccountId = command.EmployerAccountId,
                    TrainingType = command.TrainingType,
                    TrainingId = command.TrainingId,
                    TrainingStartDate = command.TrainingStartDate,
                    TrainingEndDate = command.TrainingEndDate,
                    TrainingTotalCost = command.TrainingTotalCost
                };

                await _apprenticeshipEventRepository.Create(newApprenticeshipEvent);

                _logger.Info($"Finished processing message {command.Event}", accountId: command.EmployerAccountId, providerId: command.ProviderId, @event: command.Event);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error processing message {command.Event} - {ex.Message}", accountId: command.EmployerAccountId, providerId: command.ProviderId, @event: command.Event);
                throw;
            }
        }

        private static void Validate(CreateApprenticeshipEventCommand command)
        {
            var validator = new CreateApprenticeshipEventCommandValidator();

            var validationResult = validator.Validate(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}
