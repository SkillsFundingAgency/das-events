using System;

namespace SFA.DAS.Events.Api.Models
{
    public class CreateAgreementEventRequest
    {
        public string Event { get; set; }
        public string ProviderId { get; set; }
    }
}
