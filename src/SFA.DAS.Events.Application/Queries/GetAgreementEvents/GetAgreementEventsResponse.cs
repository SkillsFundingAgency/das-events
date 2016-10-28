using System;
using System.Collections.Generic;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.Queries.GetAgreementEvents
{
    public sealed class GetAgreementEventsResponse : QueryResponse<IEnumerable<AgreementEvent>> {}
}
