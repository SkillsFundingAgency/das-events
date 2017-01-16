using System.Collections.Generic;
using MediatR;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.Commands.BulkUploadCreateApprenticeshipEvents
{
    public sealed class BulkUploadCreateApprenticeshipEventsCommand : IAsyncRequest
    {
        public IList<ApprenticeshipEvent> ApprenticeshipEvents { get; set; }
    }
}
