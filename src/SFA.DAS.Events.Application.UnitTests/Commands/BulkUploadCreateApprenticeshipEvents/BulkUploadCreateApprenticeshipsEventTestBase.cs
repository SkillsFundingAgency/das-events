using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.Commands.BulkUploadCreateApprenticeshipEvents;
using SFA.DAS.Events.Domain.Logging;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.UnitTests.Commands.BulkUploadCreateApprenticeshipEvents
{
    public abstract class BulkUploadCreateApprenticeshipsEventTestBase
    {
        protected BulkUploadCreateApprenticeshipEventsCommandHandler Handler;
        protected Mock<IApprenticeshipEventRepository> Repository;

        protected Mock<IEventsLogger> EventsLogger;
        protected Mock<BulkUploadCreateApprentieshipEventsCommandValidator> Validator;

        [SetUp]
        public void Arrange()
        {
            Repository = new Mock<IApprenticeshipEventRepository>();
            EventsLogger = new Mock<IEventsLogger>();
            Validator = new Mock<BulkUploadCreateApprentieshipEventsCommandValidator>();
            Validator.Setup(x => x.Validate(It.IsAny<BulkUploadCreateApprenticeshipEventsCommand>()))
                .Returns(new ValidationResult());

            Handler = new BulkUploadCreateApprenticeshipEventsCommandHandler(Repository.Object, EventsLogger.Object, Validator.Object);
        }
    }
}
