using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Events.Api.Types;

namespace SFA.DAS.Events.Api.Orchestrators
{
    public interface IAccountEventsOrchestrator
    {
        Task CreateEvent(AccountEvent request);
        Task<IEnumerable<AccountEventView>> GetEvents(string fromDate, string toDate, int pageSize, int pageNumber, long fromEventId);
    }
}
