using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using SFA.DAS.Events.Api.Orchestrators;

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

        //[Route("", Name = "GetAllAccountEvents")]
        //[Authorize(Roles = "ReadAccountEvent")]
        public async Task<IHttpActionResult> Get(IEnumerable<string> eventTypes, string fromDate = null, string toDate = null, int pageSize = 1000, int pageNumber = 1, long fromEventId = 0)
        {
            throw new NotImplementedException();

            //return Ok(await _orchestrator.GetEvents(fromDate, toDate, pageSize, pageNumber, fromEventId));
        }

        //[Route("", Name = "GetAllAccountEvents")]
        //[Authorize(Roles = "ReadAccountEvent")]
        public async Task<IHttpActionResult> Get(string eventType, string fromDate = null, string toDate = null, int pageSize = 1000, int pageNumber = 1, long fromEventId = 0)
        {
            throw new NotImplementedException();

            //return Ok(await _orchestrator.GetEvents(fromDate, toDate, pageSize, pageNumber, fromEventId));
        }
    }
}