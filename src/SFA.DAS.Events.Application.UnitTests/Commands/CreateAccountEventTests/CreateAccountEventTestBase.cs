using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.Commands.CreateAccountEvent;
using SFA.DAS.Events.Domain.Logging;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.UnitTests.Commands.CreateAccountEventTests
{
    public abstract class CreateAccountEventTestBase
    {
        protected CreateAccountEventCommandHandler Handler;
        protected Mock<IAccountEventRepository> Repository;
        protected CreateAccountEventCommandValidator Validator;
        protected Mock<IEventsLogger> EventsLogger;

        [SetUp]
        public void Arrange()
        {
            Repository = new Mock<IAccountEventRepository>();
            Validator = new CreateAccountEventCommandValidator();
            EventsLogger = new Mock<IEventsLogger>();

            Handler = new CreateAccountEventCommandHandler(Repository.Object, Validator, EventsLogger.Object);
        }
    }
}
