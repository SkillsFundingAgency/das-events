using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Logging;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Commands.CreateApprenticeshipEvent
{
    public sealed class CreateApprenticeshipEventCommandHandler : AsyncRequestHandler<CreateApprenticeshipEventCommand>
    {
        private readonly IEventsLogger _logger;
        private readonly IApprenticeshipEventRepository _apprenticeshipEventRepository;
        private readonly AbstractValidator<CreateApprenticeshipEventCommand> _validator;

        public CreateApprenticeshipEventCommandHandler(IApprenticeshipEventRepository apprenticeshipEventRepository,
            IEventsLogger logger,
            AbstractValidator<CreateApprenticeshipEventCommand> validator)
        {
            _apprenticeshipEventRepository = apprenticeshipEventRepository;
            _logger = logger;
            _validator = validator;
        }

        protected override async Task HandleCore(CreateApprenticeshipEventCommand command)
        {
            _logger.Info($"Received message {command.Event}", accountId: command.EmployerAccountId, providerId: command.ProviderId, @event: command.Event);

            var validationResult = _validator.Validate(command);
            if(!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

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
                    TrainingTotalCost = command.TrainingTotalCost,
                    PaymentOrder = command.PaymentOrder,
                    LegalEntityId = command.LegalEntityId,
                    LegalEntityOrganisationType = command.LegalEntityOrganisationType,
                    AccountLegalEntityPublicHashedId = command.AccountLegalEntityPublicHashedId,
                    LegalEntityName = command.LegalEntityName,
                    EffectiveFrom = command.EffectiveFrom,
                    EffectiveTo = command.EffectiveTo,
                    DateOfBirth = command.DateOfBirth,
                    PriceHistory = command.PriceHistory,
                    TransferSenderId = command.TransferSenderId,
                    TransferSenderName = command.TransferSenderName,
                    TransferApprovalStatus = command.TransferApprovalStatus,
                    TransferApprovalActionedOn = command.TransferApprovalActionedOn
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
    }
}
