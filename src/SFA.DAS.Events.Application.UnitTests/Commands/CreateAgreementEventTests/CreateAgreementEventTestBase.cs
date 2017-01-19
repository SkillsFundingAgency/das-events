using Moq;
using NUnit.Framework;

using SFA.DAS.Events.Application.Commands.CreateAgreementEvent;
using SFA.DAS.Events.Domain.Logging;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.UnitTests.Commands.CreateAgreementEventTests
{
    public abstract class CreateAgreementEventTestBase
    {
        protected CreateAgreementEventCommandHandler Handler;
        protected Mock<IAgreementEventRepository> Repository;
        protected CreateAgreementEventCommandValidator Validator;
        protected Mock<IEventsLogger> EventsLogger;

        [SetUp]
        public void Arrange()
        {
            Repository = new Mock<IAgreementEventRepository>();
            Validator = new CreateAgreementEventCommandValidator();
            EventsLogger = new Mock<IEventsLogger>();

            Handler = new CreateAgreementEventCommandHandler(Repository.Object, Validator, EventsLogger.Object);
        }
    }
}
