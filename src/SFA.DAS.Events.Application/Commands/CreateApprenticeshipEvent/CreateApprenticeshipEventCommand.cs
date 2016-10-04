using System;
using MediatR;

namespace SFA.DAS.Events.Application.Commands.CreateApprenticeshipEvent
{
    public sealed class CreateApprenticeshipEventCommand : IAsyncRequest
    {
        public string Event { get; set; }
        //todo: public string Data { get; set; }
    }
}
