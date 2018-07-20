using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Api.Extensions;
using SFA.DAS.Events.Api.Types;
using SFA.DAS.Events.Application.Commands.BulkUploadCreateApprenticeshipEvents;
using SFA.DAS.Events.Application.Commands.CreateApprenticeshipEvent;
using SFA.DAS.Events.Application.Queries.GetApprenticeshipEvents;
using SFA.DAS.Events.Domain.Logging;

namespace SFA.DAS.Events.Api.Orchestrators
{
    public class ApprenticeshipEventsOrchestrator : IApprenticeshipEventsOrchestrator
    {
        private readonly IEventsLogger _logger;
        private readonly IMediator _mediator;

        public ApprenticeshipEventsOrchestrator(IMediator mediator, IEventsLogger logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task CreateEvent(ApprenticeshipEvent request)
        {
            try
            {
                _logger.Info($"Creating Apprenticeship Event ({request.Event}) for Employer: {request.EmployerAccountId}, Provider: {request.ProviderId}", @event: request.Event, accountId: request.EmployerAccountId, providerId: request.ProviderId);

                await _mediator.SendAsync(new CreateApprenticeshipEventCommand
                {
                    Event = request.Event,
                    ApprenticeshipId = request.ApprenticeshipId,
                    PaymentStatus = (Domain.Entities.PaymentStatus) request.PaymentStatus,
                    AgreementStatus = (Domain.Entities.AgreementStatus) request.AgreementStatus,
                    ProviderId = request.ProviderId,
                    LearnerId = request.LearnerId,
                    EmployerAccountId = request.EmployerAccountId,
                    TrainingType = (Domain.Entities.TrainingTypes) request.TrainingType,
                    TrainingId = request.TrainingId,
                    TrainingStartDate = request.TrainingStartDate,
                    TrainingEndDate = request.TrainingEndDate,
                    TrainingTotalCost = request.TrainingTotalCost,
                    PaymentOrder = request.PaymentOrder,
                    LegalEntityId = request.LegalEntityId,
                    LegalEntityName = request.LegalEntityName,
                    LegalEntityOrganisationType = request.LegalEntityOrganisationType,
                    AccountLegalEntityPublicHashedId = request.AccountLegalEntityPublicHashedId,
                    EffectiveFrom = request.EffectiveFrom,
                    EffectiveTo = request.EffectiveTo,
                    DateOfBirth = request.DateOfBirth,
                    PriceHistory = request.PriceHistory?.Select(ToDomainModel).ToList() ?? new List<Domain.Entities.PriceHistory>(),
                    TransferSenderId = request.TransferSenderId,
                    TransferSenderName = request.TransferSenderName ?? string.Empty,
                    TransferApprovalStatus = (Domain.Entities.TransferApprovalStatus?) request.TransferApprovalStatus,
                    TransferApprovalActionedOn = request.TransferApprovalActionedOn
                });
            }
            catch (ValidationException ex)
            {
                _logger.Warn(ex, "Invalid request", accountId: request.EmployerAccountId, providerId: request.ProviderId, @event: request.Event);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
        }

        public async Task CreateEvents(IList<ApprenticeshipEvent> events)
        {
            try
            {
                _logger.Info($"Bulk Uploading {events.Count} Apprenticeship Event");

                await _mediator.SendAsync(new BulkUploadCreateApprenticeshipEventsCommand
                {
                    ApprenticeshipEvents = events.Select(MapFrom).ToList()
                });
            }
            catch (ValidationException ex)
            {
                _logger.Warn(ex, "Invalid apprenticeship event bulk upload request");
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<ApprenticeshipEventView>> GetEvents(string fromDate, string toDate, int pageSize, int pageNumber, long fromEventId)
        {
            try
            {
                _logger.Info($"Getting Apprenticeship Events for period: {fromDate ?? "(all)"} - {toDate ?? "(all)"}, from eventId = {(fromEventId == 0 ? "(all)" : fromEventId.ToString())}");

                fromDate = fromDate ?? new DateTime(2000, 1, 1).ToString("yyyyMMddHHmmss");
                toDate = toDate ?? DateTime.MaxValue.ToString("yyyyMMddHHmmss");

                var request = new GetApprenticeshipEventsRequest
                {
                    FromDateTime = fromDate.ParseDateTime(),
                    ToDateTime = toDate.ParseDateTime(),
                    PageSize = pageSize,
                    PageNumber = pageNumber,
                    FromEventId = fromEventId
                };

                var response = await _mediator.SendAsync(request);

                return response.Data.Select(x => new ApprenticeshipEventView
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn,
                    Event = x.Event,
                    ApprenticeshipId = x.ApprenticeshipId,
                    PaymentStatus = (PaymentStatus)x.PaymentStatus,
                    AgreementStatus = (AgreementStatus)x.AgreementStatus,
                    ProviderId = x.ProviderId,
                    LearnerId = x.LearnerId,
                    EmployerAccountId = x.EmployerAccountId,
                    TrainingType = (TrainingTypes)x.TrainingType,
                    TrainingId = x.TrainingId,
                    TrainingStartDate = x.TrainingStartDate,
                    TrainingEndDate = x.TrainingEndDate,
                    TrainingTotalCost = x.TrainingTotalCost,
                    PaymentOrder = x.PaymentOrder,
                    LegalEntityId = x.LegalEntityId,
                    LegalEntityName = x.LegalEntityName,
                    LegalEntityOrganisationType = x.LegalEntityOrganisationType,
                    AccountLegalEntityPublicHashedId = x.AccountLegalEntityPublicHashedId,
                    EffectiveFrom = x.EffectiveFrom,
                    EffectiveTo = x.EffectiveTo,
                    DateOfBirth = x.DateOfBirth,
                    PriceHistory = x.PriceHistory.Select(MapPriceHistory),
                    TransferSenderId = x.TransferSenderId,
                    TransferSenderName = x.TransferSenderName ?? string.Empty,
                    TransferApprovalStatus = (TransferApprovalStatus?) x.TransferApprovalStatus,
                    TransferApprovalActionedOn = x.TransferApprovalActionedOn
                });
            }
            catch (ValidationException ex)
            {
                _logger.Warn(ex, "Invalid request");
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
        }

        private static Domain.Entities.ApprenticeshipEvent MapFrom(ApprenticeshipEvent a)
        {
            return new Domain.Entities.ApprenticeshipEvent
            {
                AgreementStatus = (Domain.Entities.AgreementStatus)a.AgreementStatus,
                ApprenticeshipId = a.ApprenticeshipId,
                EmployerAccountId = a.EmployerAccountId,
                Event = a.Event,
                LearnerId = a.LearnerId,
                PaymentOrder = a.PaymentOrder,
                PaymentStatus = (Domain.Entities.PaymentStatus)a.PaymentStatus,
                ProviderId = a.ProviderId,
                TrainingStartDate = a.TrainingStartDate,
                TrainingEndDate = a.TrainingEndDate,
                TrainingId = a.TrainingId,
                TrainingType = (Domain.Entities.TrainingTypes)a.TrainingType,
                TrainingTotalCost = a.TrainingTotalCost,
                LegalEntityId = a.LegalEntityId,
                LegalEntityName = a.LegalEntityName,
                LegalEntityOrganisationType = a.LegalEntityOrganisationType,
                AccountLegalEntityPublicHashedId = a.AccountLegalEntityPublicHashedId,
                EffectiveFrom = a.EffectiveFrom,
                EffectiveTo = a.EffectiveTo,
                DateOfBirth = a.DateOfBirth,
                PriceHistory = a.PriceHistory?.Select(ToDomainModel).ToList() 
                    ?? new List<Domain.Entities.PriceHistory>(),
                TransferSenderId = a.TransferSenderId,
                TransferSenderName = a.TransferSenderName ?? string.Empty,
                TransferApprovalStatus = (Domain.Entities.TransferApprovalStatus?) a.TransferApprovalStatus,
                TransferApprovalActionedOn = a.TransferApprovalActionedOn
            };
        }

        private static Domain.Entities.PriceHistory ToDomainModel(PriceHistory apiType)
        {
            return new Domain.Entities.PriceHistory
            {
                TotalCost = apiType.TotalCost,
                EffectiveFrom = apiType.EffectiveFrom,
                EffectiveTo = apiType.EffectiveTo
            };
        }

        private static PriceHistory MapPriceHistory(Domain.Entities.PriceHistory source)
        {
            return new PriceHistory
            {
                EffectiveFrom = source.EffectiveFrom,
                EffectiveTo = source.EffectiveTo,
                TotalCost = source.TotalCost
            };
        }       
    }
}
