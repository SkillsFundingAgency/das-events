using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.UnitTests.Builders;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.UnitTests.Queries.GetAccountEventsTests
{
    [TestFixture]
    public class WhenIGetAccountEvents : GetAccountEventsTestBase
    {
        [Test]
        public async Task ThenAccountsAreReturned()
        {
            var request = new GetAccountEventsRequestBuilder().Build();

            var expectedAccounts = new List<AccountEvent>();

            Repository.Setup(r => r.GetByRange(request.FromDateTime, request.ToDateTime, request.PageSize, request.PageNumber, request.FromEventId)).ReturnsAsync(expectedAccounts);

            var response = await Handler.Handle(request);

            response.Data.Should().BeSameAs(expectedAccounts);
        }
    }
}
