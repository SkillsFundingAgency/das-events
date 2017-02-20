using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Events.Application.Queries.GetGenericEvents;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Api.Orchestrators
{
    public class GenericEventOrchestrator : IGenericEventOrchestrator
    {
        private readonly IMediator _mediator;

        public GenericEventOrchestrator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ICollection<GenericEvent>> GetEvents(
            string eventType, DateTime fromDate, DateTime toDate, int pageSize, int pageNumber, long fromEventId)
        {
            return await GetEvents(new[] {eventType}, fromDate, toDate, pageSize, pageNumber, fromEventId);
        }

        public async Task<ICollection<GenericEvent>> GetEvents(
            IEnumerable<string> eventTypes, DateTime fromDate, DateTime toDate, int pageSize, int pageNumber, long fromEventId)
        {
            var response = await _mediator.SendAsync(new GetGenericEventsRequest
            {
                EventTypes = eventTypes.ToList(),
                FromDateTime = fromDate,
                ToDateTime = toDate,
                PageNumber = pageNumber,
                PageSize = pageSize,
                FromEventId = fromEventId
            });

            return response?.Data.ToList();
        }
    }
}