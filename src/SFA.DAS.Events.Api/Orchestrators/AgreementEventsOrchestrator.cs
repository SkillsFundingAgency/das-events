using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using NLog;
using SFA.DAS.Events.Api.Types;
using SFA.DAS.Events.Application.Commands.CreateAgreementEvent;
using SFA.DAS.Events.Application.Queries.GetAgreementEvents;
using AgreementEvent = SFA.DAS.Events.Api.Types.AgreementEvent;

namespace SFA.DAS.Events.Api.Orchestrators
{
    public class AgreementEventsOrchestrator : IAgreementEventsOrchestrator
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly IMediator _mediator;

        public AgreementEventsOrchestrator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task CreateEvent(AgreementEvent request)
        {
            try
            {
                await _mediator.SendAsync(new CreateAgreementEventCommand
                {
                    Event = request.Event,
                    ProviderId = request.ProviderId
                });
            }
            catch (ValidationException ex)
            {
                Logger.Warn(ex, "Invalid request");
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<AgreementEventView>> GetEvents(string fromDate, string toDate, int pageSize, int pageNumber, long fromEventId)
        {
            try
            {
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
                    ProviderId = x.ProviderId
                });
            }
            catch (ValidationException ex)
            {
                Logger.Warn(ex, "Invalid request");
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                throw;
            }
        }
    }
}
