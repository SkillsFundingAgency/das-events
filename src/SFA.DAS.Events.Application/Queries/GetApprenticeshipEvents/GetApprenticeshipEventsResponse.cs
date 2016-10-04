using System;
using System.Collections.Generic;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.Queries.GetApprenticeshipEvents
{
    public sealed class GetApprenticeshipEventsResponse : QueryResponse<IEnumerable<ApprenticeshipEvent>> {}
}
