using System;

namespace SFA.DAS.Events.Domain.Entities
{
    public class AccountEvent : BaseEvent
    {
        public string Event { get; set; }
        public string ResourceUri { get; set; }
    }
}
