using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.Commands.CreateApprenticeshipEvent;
using SFA.DAS.Events.Domain.Logging;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.UnitTests.Commands.CreateApprenticeshipEventTests
{
    public abstract class CreateApprenticeshipEventTestBase
    {
        protected CreateApprenticeshipEventCommandHandler Handler;
        protected Mock<IApprenticeshipEventRepository> Repository;

        protected CreateApprenticeshipEventCommandValidator Validator;
        protected Mock<IEventsLogger> EventsLogger;

        [SetUp]
        public void Arrange()
        {
            Repository = new Mock<IApprenticeshipEventRepository>();
            Validator = new CreateApprenticeshipEventCommandValidator();
            EventsLogger = new Mock<IEventsLogger>();

            Handler = new CreateApprenticeshipEventCommandHandler(Repository.Object, Validator, EventsLogger.Object);
        }
    }
}
