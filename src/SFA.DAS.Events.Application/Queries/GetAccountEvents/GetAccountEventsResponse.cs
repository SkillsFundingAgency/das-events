using System.Collections.Generic;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.Queries.GetAccountEvents
{
    public sealed class GetAccountEventsResponse : QueryResponse<IEnumerable<AccountEvent>> {}
}
