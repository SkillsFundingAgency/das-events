using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.Commands.CreateApprenticeshipEvent;
using SFA.DAS.Events.Domain.Logging;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.UnitTests.CreateApprenticeshipEventTests
{
    public abstract class CreateApprenticeshipEventTestBase
    {
        protected CreateApprenticeshipEventCommandHandler Handler;
        protected Mock<IApprenticeshipEventRepository> Repository;
        protected Mock<IEventsLogger> EventsLogger;

        [SetUp]
        public void Arrange()
        {
            Repository = new Mock<IApprenticeshipEventRepository>();
            EventsLogger = new Mock<IEventsLogger>();

            Handler = new CreateApprenticeshipEventCommandHandler(Repository.Object, EventsLogger.Object);
        }
    }
}
