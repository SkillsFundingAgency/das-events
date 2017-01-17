using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Api.UnitTests.Builders;
using SFA.DAS.Events.Application.Queries.GetApprenticeshipEvents;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Api.UnitTests.Orchestrators.ApprenticeshipEventsOrchestratorTests
{
    [TestFixture]
    public class WhenIGetEvents : ApprenticeshipEventsOrchestratorTestBase
    {
        [Test]
        public async Task ThenTheEventsAreReturned()
        {
            var expectedEvents = new List<ApprenticeshipEvent>
            {
                new DomainApprenticeshipEventBuilder().WithCreatedOn(DateTime.Now.AddDays(-2)).WithEvent("Event 1").WithTrainingId("ABC123").Build(),
                new DomainApprenticeshipEventBuilder().WithCreatedOn(DateTime.Now.AddDays(-1)).WithEvent("Event 2").WithTrainingId("ZZZ123").Build()
            };

            var toDateTime = DateTime.Now;
            var fromDateTime = DateTime.Now.AddDays(-2);
            var fromEventId = 22;
            var pageNumber = 2;
            var pageSize = 100;

            Mediator.Setup(m => m.SendAsync(It.Is<GetApprenticeshipEventsRequest>(x => RequestMatchesParameters(x, toDateTime, fromDateTime, fromEventId, pageNumber, pageSize))))
                .ReturnsAsync(new GetApprenticeshipEventsResponse {Data = expectedEvents});

            var response = await Orchestrator.GetEvents(fromDateTime.ToString("yyyyMMddHHmmss"), toDateTime.ToString("yyyyMMddHHmmss"), pageSize, pageNumber, fromEventId);

            response.ShouldAllBeEquivalentTo(expectedEvents);
        }

        [Test]
        public async Task AndValidationFails()
        {
            var validationException = new ValidationException("Exception");
            Mediator.Setup(m => m.SendAsync(It.IsAny<GetApprenticeshipEventsRequest>())).ThrowsAsync(validationException);

            Assert.ThrowsAsync<ValidationException>(() => Orchestrator.GetEvents(DateTime.Now.AddDays(-30).ToString("yyyyMMddHHmmss"), DateTime.Now.ToString("yyyyMMddHHmmss"), 100, 1, 123));

            EventsLogger.Verify(x => x.Warn(validationException, "Invalid request", null, null, null));
        }

        [Test]
        public async Task AndAnExceptionOccurs()
        {
            var exception = new Exception("Exception");
            Mediator.Setup(m => m.SendAsync(It.IsAny<GetApprenticeshipEventsRequest>())).ThrowsAsync(exception);

            Assert.ThrowsAsync<Exception>(() => Orchestrator.GetEvents(DateTime.Now.AddDays(-30).ToString("yyyyMMddHHmmss"), DateTime.Now.ToString("yyyyMMddHHmmss"), 100, 1, 123));

            EventsLogger.Verify(x => x.Error(exception, exception.Message, null, null, null));
        }

        private static bool RequestMatchesParameters(GetApprenticeshipEventsRequest x, DateTime toDateTime, DateTime fromDateTime, int fromEventId, int pageNumber, int pageSize)
        {
            return x.ToDateTime.ToString("yyyyMMddHHmmss") == toDateTime.ToString("yyyyMMddHHmmss") &&
                   x.FromDateTime.ToString("yyyyMMddHHmmss") == fromDateTime.ToString("yyyyMMddHHmmss") &&
                   x.FromEventId == fromEventId &&
                   x.PageNumber == pageNumber && 
                   x.PageSize == pageSize;
        }
    }
}
