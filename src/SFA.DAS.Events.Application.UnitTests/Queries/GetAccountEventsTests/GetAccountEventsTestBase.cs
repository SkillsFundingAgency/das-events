using Moq;
using NUnit.Framework;

using SFA.DAS.Events.Application.Queries.GetAccountEvents;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.UnitTests.Queries.GetAccountEventsTests
{
    public abstract class GetAccountEventsTestBase
    {
        protected Mock<IAccountEventRepository> Repository;
        protected GetAccountEventsRequestValidator Validator;
        protected GetAccountEventsQueryHandler Handler;

        [SetUp]
        public void Arrange()
        {
            Repository = new Mock<IAccountEventRepository>();
            Validator = new GetAccountEventsRequestValidator();

            Handler = new GetAccountEventsQueryHandler(Repository.Object, Validator);
        }
    }
}
