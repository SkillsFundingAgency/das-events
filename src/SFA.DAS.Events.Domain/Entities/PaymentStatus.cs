using System;

namespace SFA.DAS.Events.Domain.Entities
{
    public enum PaymentStatus
    {
        PendingApproval = 0,
        Active = 1,
        Paused = 2,
        Cancelled = 3,
        Completed = 4,
        Deleted = 5
    }
}
