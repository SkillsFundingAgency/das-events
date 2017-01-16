using MediatR;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Api.Orchestrators;
using SFA.DAS.Events.Domain.Logging;

namespace SFA.DAS.Events.Api.UnitTests.Orchestrators.AgreementEventsOrchestratorTests
{
    public abstract class AgreementEventsOrchestratorTestBase
    {
        protected Mock<IMediator> Mediator;
        protected Mock<IEventsLogger> EventsLogger;
        protected AgreementEventsOrchestrator Orchestrator;

        [SetUp]
        public void Arrange()
        {
            Mediator = new Mock<IMediator>();
            EventsLogger = new Mock<IEventsLogger>();

            Orchestrator = new AgreementEventsOrchestrator(Mediator.Object, EventsLogger.Object);
        }
    }
}
