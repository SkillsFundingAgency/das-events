using MediatR;

namespace SFA.DAS.Events.Application.Commands.CreateAccountEvent
{
    public sealed class CreateAccountEventCommand : IAsyncRequest
    {
        public string Event { get; set; }
        public string EmployerAccountId { get; set; }
    }
}
