using Moq;
using NUnit.Framework;

using SFA.DAS.Events.Application.Queries.GetAgreementEvents;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.UnitTests.Queries.GetAgreementEventsTests
{
    public abstract class GetAgreementEventsTestBase
    {
        protected Mock<IAgreementEventRepository> Repository;
        protected GetAgreementEventsRequestValidator Validator;
        protected GetAgreementEventsQueryHandler Handler;

        [SetUp]
        public void Arrange()
        {
            Repository = new Mock<IAgreementEventRepository>();
            Validator = new GetAgreementEventsRequestValidator();

            Handler = new GetAgreementEventsQueryHandler(Repository.Object, Validator);
        }
    }
}
