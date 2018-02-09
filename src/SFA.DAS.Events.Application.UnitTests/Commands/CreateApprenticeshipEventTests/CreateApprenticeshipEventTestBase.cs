using FluentValidation;
using FluentValidation.Results;
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

        protected Mock<IEventsLogger> EventsLogger;
        protected Mock<CreateApprenticeshipEventCommandValidator> Validator;

        [SetUp]
        public void Arrange()
        {
            Repository = new Mock<IApprenticeshipEventRepository>();
            EventsLogger = new Mock<IEventsLogger>();
            Validator = new Mock<CreateApprenticeshipEventCommandValidator>();
            Validator.Setup(x => x.Validate(It.IsAny<CreateApprenticeshipEventCommand>()))
                .Returns(new ValidationResult());

            Handler = new CreateApprenticeshipEventCommandHandler(Repository.Object, EventsLogger.Object, Validator.Object);
        }
    }
}
