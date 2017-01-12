using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Api.Types;
using SFA.DAS.Events.Application.Commands.CreateAccountEvent;
using SFA.DAS.Events.Application.Commands.CreateAgreementEvent;
using SFA.DAS.Events.Application.Queries.GetAccountEvents;
using SFA.DAS.Events.Application.Queries.GetAgreementEvents;
using SFA.DAS.Events.Domain.Logging;
using AccountEvent = SFA.DAS.Events.Api.Types.AccountEvent;

namespace SFA.DAS.Events.Api.Orchestrators
{
    public class AccountEventsOrchestrator : IAccountEventsOrchestrator
    {
        private readonly IEventsLogger _logger;
        private readonly IMediator _mediator;

        public AccountEventsOrchestrator(IMediator mediator, IEventsLogger logger)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            _mediator = mediator;
            _logger = logger;
        }

        public async Task CreateEvent(AccountEvent request)
        {
            try
            {
                _logger.Info($"Creating Account Event ({request.Event})", accountId: request.EmployerAccountId, @event: request.Event);

                await _mediator.SendAsync(new CreateAccountEventCommand
                {
                    Event = request.Event,
                    EmployerAccountId = request.EmployerAccountId
                });
            }
            catch (ValidationException ex)
            {
                _logger.Warn(ex, "Invalid request", accountId: request.EmployerAccountId, @event: request.Event);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<AccountEventView>> GetEvents(string fromDate, string toDate, int pageSize, int pageNumber, long fromEventId)
        {
            try
            {
                _logger.Info($"Getting Account Events for period: {fromDate ?? "(all)"} - {toDate ?? "(all)"}, from eventId = {(fromEventId == 0 ? "(all)" : fromEventId.ToString())}");

                fromDate = fromDate ?? new DateTime(2000, 1, 1).ToString("yyyyMMddHHmmss");
                toDate = toDate ?? DateTime.MaxValue.ToString("yyyyMMddHHmmss");

                var request = new GetAccountEventsRequest
                {
                    FromDateTime = fromDate.ParseDateTime(),
                    ToDateTime = toDate.ParseDateTime(),
                    PageSize = pageSize,
                    PageNumber = pageNumber,
                    FromEventId = fromEventId
                };

                var response = await _mediator.SendAsync(request);

                return response.Data.Select(x => new AccountEventView
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn,
                    Event = x.Event,
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
