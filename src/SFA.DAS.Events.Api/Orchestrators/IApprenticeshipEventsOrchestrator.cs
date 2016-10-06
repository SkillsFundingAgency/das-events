using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Events.Api.Models;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Api.Orchestrators
{
    public interface IApprenticeshipEventsOrchestrator
    {
        Task CreateEvent(CreateApprenticeshipEventRequest request);
        Task<IEnumerable<ApprenticeshipEvent>> GetEvents(string fromDate, string toDate, int pageSize, int pageNumber, long fromEventId);
    }
}
