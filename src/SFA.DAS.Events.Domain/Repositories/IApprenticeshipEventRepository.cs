using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Domain.Repositories
{
    public interface IApprenticeshipEventRepository : IEventRepository<ApprenticeshipEvent>
    {
        Task BulkUploadApprenticeshipEvents(IList<ApprenticeshipEvent> apprenticeshipEvents);
    }
}
