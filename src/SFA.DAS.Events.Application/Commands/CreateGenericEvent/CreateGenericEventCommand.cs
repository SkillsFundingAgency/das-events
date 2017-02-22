using MediatR;

namespace SFA.DAS.Events.Application.Commands.CreateGenericEvent
{
    public class CreateGenericEventCommand : IAsyncRequest
    {
        public string Event { get; set; }
        public string Type { get; set; }
        public string Payload { get; set; }
    }
}
