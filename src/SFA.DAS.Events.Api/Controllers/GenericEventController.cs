using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using SFA.DAS.Events.Api.Extensions;
using SFA.DAS.Events.Api.Orchestrators;
using SFA.DAS.Events.Api.Types;


namespace SFA.DAS.Events.Api.Controllers
{
    [RoutePrefix("api/events")]
    public class GenericEventController : ApiController
    {
        private readonly IGenericEventOrchestrator _orchestrator;

        public GenericEventController(IGenericEventOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        [Route("create")]
        [Authorize(Roles = "WriteGenericEvent")]
        public async Task<IHttpActionResult> CreateGenericEvent(GenericEvent @event)
        {
            await _orchestrator.CreateEvent(@event);
            
            // 201 for list of all events
            return Created("GetSinceEvent", @event);
        }

        [Route("getByDateRange")]
        [Authorize(Roles = "ReadGenericEvent")]
        public async Task<IHttpActionResult> GetByDateRange(IEnumerable<string> eventTypes, string fromDate = null, string toDate = null, int pageSize = 1000, int pageNumber = 1)
        {
            return Ok(await _orchestrator.GetEventsByDateRange(eventTypes, fromDate.ParseDateTime(), toDate.ParseDateTime(), pageSize, pageNumber));
        }

        [Route("getByDateRange")]
        [Authorize(Roles = "ReadGenericEvent")]
        public async Task<IHttpActionResult> GetByDateRange(string eventType, string fromDate = null, string toDate = null, int pageSize = 1000, int pageNumber = 1)
        {
            var types = new[] {eventType};

            return Ok(await _orchestrator.GetEventsByDateRange(types, fromDate.ParseDateTime(), toDate.ParseDateTime(), pageSize, pageNumber));
        }

        [Route("getSinceEvent")]
        [Authorize(Roles = "ReadGenericEvent")]
        public async Task<IHttpActionResult> GetSinceEvent(IEnumerable<string> eventTypes, int pageSize = 1000, int pageNumber = 1, long fromEventId = 0)
        {
            return Ok(await _orchestrator.GetEventsSinceEvent(eventTypes, fromEventId, pageSize, pageNumber));
        }

        [Route("getSinceEvent")]
        [Authorize(Roles = "ReadGenericEvent")]
        public async Task<IHttpActionResult> GetSinceEvent(string eventType, int pageSize = 1000, int pageNumber = 1, long fromEventId = 0)
        {
            var types = new[] { eventType };

            return Ok(await _orchestrator.GetEventsSinceEvent(types, fromEventId, pageSize, pageNumber));
        }
    }
}