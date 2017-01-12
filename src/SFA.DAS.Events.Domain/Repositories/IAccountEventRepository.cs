using System;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Domain.Repositories
{
    public interface IAccountEventRepository : IEventRepository<AccountEvent> {}
}
