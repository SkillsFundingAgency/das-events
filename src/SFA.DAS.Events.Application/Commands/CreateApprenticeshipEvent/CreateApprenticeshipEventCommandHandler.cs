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

        protected override async Task HandleCore(CreateApprenticeshipEventCommand command)
        {
            Logger.Info($"Received message {command.Event}");

            Validate(command);

            try
            {
                var newApprenticeshipEvent = new ApprenticeshipEvent
                {
                    Event = command.Event,
                    CreatedOn = DateTime.UtcNow,
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

                Logger.Info($"Finished processing message {command.Event}");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Error processing message {command.Event} - {ex.Message}");
                throw;
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
