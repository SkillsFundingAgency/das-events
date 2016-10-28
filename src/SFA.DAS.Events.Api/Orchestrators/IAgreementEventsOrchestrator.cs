using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Events.Api.Types;

namespace SFA.DAS.Events.Api.Orchestrators
{
    public interface IAgreementEventsOrchestrator
    {
        Task CreateEvent(AgreementEvent request);
        Task<IEnumerable<AgreementEventView>> GetEvents(string fromDate, string toDate, int pageSize, int pageNumber, long fromEventId);
    }
}
