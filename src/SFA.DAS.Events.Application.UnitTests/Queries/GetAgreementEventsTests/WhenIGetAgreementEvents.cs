using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.UnitTests.Builders;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.UnitTests.Queries.GetAgreementEventsTests
{
    [TestFixture]
    public class WhenIGetAgreementEvents : GetAgreementEventsTestBase
    {
        [Test]
        public async Task ThenAccountsAreReturned()
        {
            var request = new GetAgreementEventsRequestBuilder().Build();

            var expectedAgreements = new List<AgreementEvent>();

            Repository.Setup(r => r.GetByRange(request.FromDateTime, request.ToDateTime, request.PageSize, request.PageNumber, request.FromEventId)).ReturnsAsync(expectedAgreements);

            var response = await Handler.Handle(request);

            response.Data.Should().BeSameAs(expectedAgreements);
        }
    }
}
