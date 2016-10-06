using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using NLog;
using SFA.DAS.Events.Api.Models;
using SFA.DAS.Events.Application.Commands.CreateApprenticeshipEvent;
using SFA.DAS.Events.Application.Queries.GetApprenticeshipEvents;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Api.Orchestrators
{
    public class ApprenticeshipEventsOrchestrator : IApprenticeshipEventsOrchestrator
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IMediator _mediator;

        public ApprenticeshipEventsOrchestrator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task CreateEvent(CreateApprenticeshipEventRequest request)
        {
            try
            {
                await _mediator.SendAsync(new CreateApprenticeshipEventCommand
                {
                    Event = request.Event,
                    ApprenticeshipId = request.ApprenticeshipId,
                    PaymentStatus = request.PaymentStatus,
                    AgreementStatus = request.AgreementStatus,
                    ProviderId = request.ProviderId,
                    LearnerId = request.LearnerId,
                    EmployerAccountId = request.EmployerAccountId,
                    TrainingType = request.TrainingType,
                    TrainingId = request.TrainingId,
                    TrainingStartDate = request.TrainingStartDate,
                    TrainingEndDate = request.TrainingEndDate,
                    TrainingTotalCost = request.TrainingTotalCost
                });
            }
            catch (ValidationException ex)
            {
                //todo: this is not logging to eventhub
                Logger.Warn(ex, "Invalid request");
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<ApprenticeshipEvent>> GetEvents(string fromDate, string toDate, int pageSize, int pageNumber, long fromEventId)
        {
            try
            {
                fromDate = fromDate ?? new DateTime(2000, 1, 1).ToString("yyyyMMddHHmmss");
                toDate = toDate ?? DateTime.MaxValue.ToString("yyyyMMddHHmmss");

                var request = new GetApprenticeshipEventsRequest
                {
                    FromDateTime = ParseDateTime(fromDate),
                    ToDateTime = ParseDateTime(toDate),
                    PageSize = pageSize,
                    PageNumber = pageNumber,
                    FromEventId = fromEventId
                };

                var response = await _mediator.SendAsync(request);

                return response.Data;
            }
            catch (ValidationException ex)
            {
                //todo: this is not logging to eventhub
                Logger.Warn(ex, "Invalid request");
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                throw;
            }
        }

        private static DateTime ParseDateTime(string datetime)
        {
            try
            {
                return DateTime.ParseExact(datetime, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw new ValidationException("Bad date format");
            }
        }
    }
}
