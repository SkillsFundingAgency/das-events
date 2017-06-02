using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.Events.Api.Types;
using SFA.DAS.Events.Application.Commands.CreateGenericEvent;
using SFA.DAS.Events.Application.Queries.GetGenericEventsByDateRange;
using SFA.DAS.Events.Application.Queries.GetGenericEventsByResourceId;
using SFA.DAS.Events.Application.Queries.GetGenericEventsByResourceUri;
using SFA.DAS.Events.Application.Queries.GetGenericEventsSinceEvent;

namespace SFA.DAS.Events.Api.Orchestrators
{
    public class GenericEventOrchestrator : IGenericEventOrchestrator
    {
        private readonly IMediator _mediator;

        public GenericEventOrchestrator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task CreateEvent(GenericEvent @event)
        {
            await _mediator.SendAsync(new CreateGenericEventCommand
            {
                Type = @event.Type,
                Payload = @event.Payload
            });
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

            return response?.Data.Select(MapFrom).ToList();
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

            return response?.Data.Select(MapFrom).ToList();
        }

        private GenericEvent MapFrom(Domain.Entities.GenericEvent @event)
        {
            return new GenericEvent
            {
                Id = @event.Id,
                CreatedOn = @event.CreatedOn,
                Payload = @event.Payload,
                Type = @event.Type
            };
        }

        public async Task<ICollection<GenericEvent>> GetEventsByResourceId(string resourceType, string resourceId, DateTime? fromDate, DateTime? toDate, int pageSize, int pageNumber)
        {
            var response = await _mediator.SendAsync(new GetGenericEventsByResourceIdRequest
            {
                ResourceType = resourceType,
                ResourceId = resourceId,
                FromDateTime = fromDate,
                ToDateTime = toDate,
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            return response?.Data.Select(MapFrom).ToList();
        }

        public async Task<ICollection<GenericEvent>> GetEventsByResourceUri(string resourceUri, DateTime? fromDate, DateTime? toDate, int pageSize, int pageNumber)
        {
            var response = await _mediator.SendAsync(new GetGenericEventsByResourceUriRequest
            {
                ResourceUri = resourceUri,
                FromDateTime = fromDate,
                ToDateTime = toDate,
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            return response?.Data.Select(MapFrom).ToList();
        }
    }
}