using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Api.Types;
using SFA.DAS.Events.Application.Commands.CreateAgreementEvent;
using SFA.DAS.Events.Application.Queries.GetAgreementEvents;
using SFA.DAS.Events.Domain.Logging;
using AgreementEvent = SFA.DAS.Events.Api.Types.AgreementEvent;

namespace SFA.DAS.Events.Api.Orchestrators
{
    public class AgreementEventsOrchestrator : IAgreementEventsOrchestrator
    {
        private readonly IEventsLogger _logger;
        private readonly IMediator _mediator;

        public AgreementEventsOrchestrator(IMediator mediator, IEventsLogger logger)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            _mediator = mediator;
            _logger = logger;
        }

        public async Task CreateEvent(AgreementEvent request)
        {
            try
            {
                _logger.Info($"Creating Agreement Event ({request.Event}) for Employer: {request.EmployerAccountId}, Provider: {request.ProviderId}", accountId: request.EmployerAccountId, providerId: request.ProviderId, @event: request.Event);

                await _mediator.SendAsync(new CreateAgreementEventCommand
                {
                    Event = request.Event,
                    ProviderId = request.ProviderId,
                    EmployerAccountId = request.EmployerAccountId
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

        public async Task<IEnumerable<AgreementEventView>> GetEvents(string fromDate, string toDate, int pageSize, int pageNumber, long fromEventId)
        {
            try
            {
                _logger.Info($"Getting Agreement Events for period: {fromDate ?? "(all)"} - {toDate ?? "(all)"}, from eventId = {(fromEventId == 0 ? "(all)" : fromEventId.ToString())}");

                fromDate = fromDate ?? new DateTime(2000, 1, 1).ToString("yyyyMMddHHmmss");
                toDate = toDate ?? DateTime.MaxValue.ToString("yyyyMMddHHmmss");

                var request = new GetAgreementEventsRequest
                {
                    FromDateTime = fromDate.ParseDateTime(),
                    ToDateTime = toDate.ParseDateTime(),
                    PageSize = pageSize,
                    PageNumber = pageNumber,
                    FromEventId = fromEventId
                };

                var response = await _mediator.SendAsync(request);

                return response.Data.Select(x => new AgreementEventView
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn,
                    Event = x.Event,
                    ProviderId = x.ProviderId,
                    EmployerAccountId = x.EmployerAccountId
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
    }
}
