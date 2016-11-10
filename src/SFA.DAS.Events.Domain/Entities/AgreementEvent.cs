using System;

namespace SFA.DAS.Events.Domain.Entities
{
    public class AgreementEvent : BaseEvent
    {
        public string ProviderId { get; set; }
        public string EmployerAccountId { get; set; }
    }
}
