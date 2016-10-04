using System;

namespace SFA.DAS.Events.Domain.Entities
{
    public abstract class BaseEvent
    {
        public long Id { get; set; }
        public EventTypes EventType { get; set; }
        public string Event { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Data { get; set; }
    }
}
