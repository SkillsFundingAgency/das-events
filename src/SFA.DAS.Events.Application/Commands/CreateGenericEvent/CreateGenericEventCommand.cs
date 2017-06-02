using MediatR;

namespace SFA.DAS.Events.Application.Commands.CreateGenericEvent
{
    public class CreateGenericEventCommand : IAsyncRequest
    {
        public string Type { get; set; }
        public string Payload { get; set; }
        public string ResourceType { get; set; }
        public string ResourceId { get; set; }
        public string ResourceUri { get; set; }
    }
}
