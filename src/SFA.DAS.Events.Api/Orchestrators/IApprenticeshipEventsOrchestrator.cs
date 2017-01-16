using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Events.Api.Types;

namespace SFA.DAS.Events.Api.Orchestrators
{
    public interface IApprenticeshipEventsOrchestrator
    {
        Task CreateEvent(ApprenticeshipEvent request);
        Task<IEnumerable<ApprenticeshipEventView>> GetEvents(string fromDate, string toDate, int pageSize, int pageNumber, long fromEventId);
        Task CreateEvents(IList<ApprenticeshipEvent> events);
    }
}
