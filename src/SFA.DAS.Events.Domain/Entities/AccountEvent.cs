using System;

namespace SFA.DAS.Events.Domain.Entities
{
    public class AccountEvent : BaseEvent
    {
        public string EmployerAccountId { get; set; }
    }
}
