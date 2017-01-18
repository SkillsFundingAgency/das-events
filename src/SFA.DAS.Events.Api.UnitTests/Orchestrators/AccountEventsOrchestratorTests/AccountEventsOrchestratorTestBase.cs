using MediatR;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Api.Orchestrators;
using SFA.DAS.Events.Domain.Logging;

namespace SFA.DAS.Events.Api.UnitTests.Orchestrators.AccountEventsOrchestratorTests
{
    public abstract class AccountEventsOrchestratorTestBase
    {
        protected Mock<IMediator> Mediator;
        protected Mock<IEventsLogger> EventsLogger;
        protected AccountEventsOrchestrator Orchestrator;

        [SetUp]
        public void Arrange()
        {
            Mediator = new Mock<IMediator>();
            EventsLogger = new Mock<IEventsLogger>();

            Orchestrator = new AccountEventsOrchestrator(Mediator.Object, EventsLogger.Object);
        }
    }
}
