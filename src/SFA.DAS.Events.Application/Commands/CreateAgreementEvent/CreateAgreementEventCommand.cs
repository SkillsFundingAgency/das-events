using System;
using MediatR;

namespace SFA.DAS.Events.Application.Commands.CreateAgreementEvent
{
    public sealed class CreateAgreementEventCommand : IAsyncRequest
    {
        public string Event { get; set; }
        public string ProviderId { get; set; }
        public string EmployerAccountId { get; set; }
    }
}
