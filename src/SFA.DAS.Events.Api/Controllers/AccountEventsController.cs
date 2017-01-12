using System;
using System.Threading.Tasks;
using System.Web.Http;
using NLog.Time;
using SFA.DAS.Events.Api.Orchestrators;
using SFA.DAS.Events.Api.Types;

namespace SFA.DAS.Events.Api.Controllers
{
    [RoutePrefix("api/events/accounts")]
    public class AccountEventsController : ApiController
    {
        private readonly IAccountEventsOrchestrator _orchestrator;

        public AccountEventsController(IAccountEventsOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        [Route("", Name = "GetAllAccountEvents")]
        [Authorize(Roles = "ReadAccountEvent")]
        public async Task<IHttpActionResult> Get(string fromDate = null, string toDate = null, int pageSize = 1000, int pageNumber = 1, long fromEventId = 0)
        {
            return Ok(await _orchestrator.GetEvents(fromDate, toDate, pageSize, pageNumber, fromEventId));
        }

        [Route("")]
        [Authorize(Roles = "StoreAccountEvent")]
        public async Task<IHttpActionResult> Post(AccountEvent request)
        {
            await _orchestrator.CreateEvent(request);

            // 201 for list of all events
            return CreatedAtRoute("GetAllAccountEvents", new {}, default(AccountEvent));
        }
    }
}
