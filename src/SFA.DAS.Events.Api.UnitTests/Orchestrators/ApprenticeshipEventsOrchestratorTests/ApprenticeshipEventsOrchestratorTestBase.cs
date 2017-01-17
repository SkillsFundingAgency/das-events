using MediatR;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Api.Orchestrators;
using SFA.DAS.Events.Domain.Logging;

namespace SFA.DAS.Events.Api.UnitTests.Orchestrators.ApprenticeshipEventsOrchestratorTests
{
    public abstract class ApprenticeshipEventsOrchestratorTestBase
    {
        protected Mock<IMediator> Mediator;
        protected Mock<IEventsLogger> EventsLogger;
        protected ApprenticeshipEventsOrchestrator Orchestrator;

        [SetUp]
        public void Arrange()
        {
            Mediator = new Mock<IMediator>();
            EventsLogger = new Mock<IEventsLogger>();

            Orchestrator = new ApprenticeshipEventsOrchestrator(Mediator.Object, EventsLogger.Object);
        }
    }
}
