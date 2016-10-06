﻿using System;
using System.Threading.Tasks;
using System.Web.Http;
using SFA.DAS.Events.Api.Models;
using SFA.DAS.Events.Api.Orchestrators;

namespace SFA.DAS.Events.Api.Controllers
{
    [RoutePrefix("api/events/apprenticeships")]
    public class ApprenticeshipEventsController : ApiController
    {
        private readonly IApprenticeshipEventsOrchestrator _orchestrator;

        public ApprenticeshipEventsController(IApprenticeshipEventsOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        [Route("", Name = "GetAllEvents")]
        public async Task<IHttpActionResult> Get(string fromDate = null, string toDate = null, int pageSize = 1000, int pageNumber = 1, long fromEventId = 0)
        {
            return Ok(await _orchestrator.GetEvents(fromDate, toDate, pageSize, pageNumber, fromEventId));
        }

        [Route("")]
        public async Task<IHttpActionResult> Post(CreateApprenticeshipEventRequest request)
        {
            await _orchestrator.CreateEvent(request);

            // 201 for list of all events
            return CreatedAtRoute("GetAllEvents", new {}, default(CreateApprenticeshipEventRequest));
        }
    }
}
