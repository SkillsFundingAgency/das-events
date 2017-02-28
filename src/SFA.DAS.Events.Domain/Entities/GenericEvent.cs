using System;

namespace SFA.DAS.Events.Domain.Entities
{
    public class GenericEvent : BaseEvent
    {
        public string Type { get; set; }
        public string Payload { get; set; } 
    }
}
