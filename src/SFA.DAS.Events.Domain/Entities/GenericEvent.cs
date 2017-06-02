using System;

namespace SFA.DAS.Events.Domain.Entities
{
    public class GenericEvent : BaseEvent
    {
        public string Type { get; set; }
        public string Payload { get; set; } 
        public string ResourceType { get; set; }
        public string ResourceId { get; set; }
        public string ResourceUri { get; set; }
    }
}
