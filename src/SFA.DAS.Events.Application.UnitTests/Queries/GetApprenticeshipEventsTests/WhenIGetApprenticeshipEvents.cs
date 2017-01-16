using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.UnitTests.Builders;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.UnitTests.Queries.GetApprenticeshipEventsTests
{
    [TestFixture]
    public class WhenIGetApprenticeshipEvents : GetApprenticeshipEventsTestBase
    {
        [Test]
        public async Task ThenApprenticeshipAreReturned()
        {
            var request = new GetApprenticeshipEventsRequestBuilder().Build();

            var expectedApprenticeship = new List<ApprenticeshipEvent>();

            Repository.Setup(r => r.GetByRange(request.FromDateTime, request.ToDateTime, request.PageSize, request.PageNumber, request.FromEventId)).ReturnsAsync(expectedApprenticeship);

            var response = await Handler.Handle(request);

            response.Data.Should().BeSameAs(expectedApprenticeship);
        }
    }
}
