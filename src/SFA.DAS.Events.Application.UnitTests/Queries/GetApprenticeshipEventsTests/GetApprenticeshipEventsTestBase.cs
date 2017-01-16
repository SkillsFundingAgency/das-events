using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.Queries.GetApprenticeshipEvents;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.UnitTests.Queries.GetApprenticeshipEventsTests
{
    public abstract class GetApprenticeshipEventsTestBase
    {
        protected Mock<IApprenticeshipEventRepository> Repository;
        protected GetApprenticeshipEventsQueryHandler Handler;

        [SetUp]
        public void Arrange()
        {
            Repository = new Mock<IApprenticeshipEventRepository>();
            Handler = new GetApprenticeshipEventsQueryHandler(Repository.Object);
        }
    }
}
