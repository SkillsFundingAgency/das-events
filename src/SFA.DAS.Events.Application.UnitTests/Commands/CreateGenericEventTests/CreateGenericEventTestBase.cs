using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.Commands.CreateGenericEvent;
using SFA.DAS.Events.Domain.Logging;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.UnitTests.Commands.CreateGenericEventTests
{
    public abstract class CreateGenericEventTestBase
    {
        protected CreateGenericEventCommandHandler Handler;
        protected Mock<IGenericEventRepository> Repository;
        protected CreateGenericEventCommandValidator Validator;
        protected Mock<IEventsLogger> EventsLogger;

        [SetUp]
        public void Arrange()
        {
            Repository = new Mock<IGenericEventRepository>();
            Validator = new CreateGenericEventCommandValidator();
            EventsLogger = new Mock<IEventsLogger>();

            Handler = new CreateGenericEventCommandHandler(Repository.Object, Validator, EventsLogger.Object);
        }
    }
}
