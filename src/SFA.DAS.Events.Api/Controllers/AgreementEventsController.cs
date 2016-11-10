using System;
using System.Threading.Tasks;
using System.Web.Http;
using SFA.DAS.Events.Api.Models;
using SFA.DAS.Events.Api.Orchestrators;
using SFA.DAS.Events.Api.Types;

namespace SFA.DAS.Events.Api.Controllers
{
    [RoutePrefix("api/events/engagements")]
    public class AgreementEventsController : ApiController
    {
        private readonly IAgreementEventsOrchestrator _orchestrator;

        public AgreementEventsController(IAgreementEventsOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        [Route("", Name = "GetAllAgreementEvents")]
        [Authorize(Roles = "ReadAgreementEvent")]
        public async Task<IHttpActionResult> Get(string fromDate = null, string toDate = null, int pageSize = 1000, int pageNumber = 1, long fromEventId = 0)
        {
            return Ok(await _orchestrator.GetEvents(fromDate, toDate, pageSize, pageNumber, fromEventId));
        }

        [Route("")]
        [Authorize(Roles = "StoreAgreementEvent")]
        public async Task<IHttpActionResult> Post(AgreementEvent request)
        {
            await _orchestrator.CreateEvent(request);

            // 201 for list of all events
            return CreatedAtRoute("GetAllAgreementEvents", new {}, default(CreateAgreementEventRequest));
        }
    }
}
