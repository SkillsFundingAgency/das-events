using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Events.Application.Queries.GetGenericEventsByDateRange;
using SFA.DAS.Events.Application.Queries.GetGenericEventsSinceEvent;
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

        public async Task<ICollection<GenericEvent>> GetEventsByDateRange(IEnumerable<string> eventTypes, DateTime fromDate, DateTime toDate, int pageSize, int pageNumber)
        {
            var response = await _mediator.SendAsync(new GetGenericEventsByDateRangeRequest
            {
                EventTypes = eventTypes,
                FromDateTime = fromDate,
                ToDateTime = toDate,
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            return response?.Data.ToList();
        }

        public async Task<ICollection<GenericEvent>> GetEventsSinceEvent(IEnumerable<string> eventTypes, long fromEventId, int pageSize, int pageNumber)
        {
            var response = await _mediator.SendAsync(new GetGenericEventsSinceEventRequest
            {
                EventTypes = eventTypes,
                FromEventId = fromEventId,
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            return response?.Data.ToList();
        }
    }
}