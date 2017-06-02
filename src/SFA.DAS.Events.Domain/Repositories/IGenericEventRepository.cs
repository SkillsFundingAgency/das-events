using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Domain.Repositories
{
    public interface IGenericEventRepository
    {
        Task Create(GenericEvent @event);

        Task<IEnumerable<GenericEvent>> GetByDateRange(
            IEnumerable<string> eventTypes, 
            DateTime fromDate, 
            DateTime toDate, 
            int pageSize, 
            int pageNumber);

        Task<IEnumerable<GenericEvent>> GetSinceEvent(
            IEnumerable<string> eventTypes, 
            long fromEventId,
            int pageSize, 
            int pageNumber);

        Task<IEnumerable<GenericEvent>> GetByResourceId(
            string resourceType,
            string resourceId,
            DateTime? fromDate,
            DateTime? toDate,
            int pageSize,
            int pageNumber);

        Task<IEnumerable<GenericEvent>> GetByResourceUri(
            string resourceUri,
            DateTime? fromDate,
            DateTime? toDate,
            int pageSize,
            int pageNumber);
    }
}
