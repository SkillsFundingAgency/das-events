using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.Queries.GetAgreementEvents;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Api.UnitTests.Orchestrators.AgreementEventsOrchestratorTests
{
    [TestFixture]
    public class WhenIGetEvents : AgreementEventsOrchestratorTestBase
    {
        [Test]
        public async Task ThenTheEventsAreReturned()
        {
            var expectedEvents = new List<AgreementEvent>
            {
                new AgreementEvent { CreatedOn = DateTime.Now.AddDays(-2), ContractType = "MainProvider", Event = "Event 1", Id = 23, ProviderId = "ZZZ123" },
                new AgreementEvent { CreatedOn = DateTime.Now.AddDays(-1), ContractType = "MainProvider", Event = "Event 2", Id = 24, ProviderId = "ZZZ999" },
            };

            var toDateTime = DateTime.Now;
            var fromDateTime = DateTime.Now.AddDays(-2);
            var fromEventId = 22;
            var pageNumber = 2;
            var pageSize = 100;

            Mediator.Setup(m => m.SendAsync(It.Is<GetAgreementEventsRequest>(x => RequestMatchesParameters(x, toDateTime, fromDateTime, fromEventId, pageNumber, pageSize))))
                .ReturnsAsync(new GetAgreementEventsResponse {Data = expectedEvents});

            var response = await Orchestrator.GetEvents(fromDateTime.ToString("yyyyMMddHHmmss"), toDateTime.ToString("yyyyMMddHHmmss"), pageSize, pageNumber, fromEventId);

            response.ShouldAllBeEquivalentTo(expectedEvents, opts => opts.ExcludingMissingMembers());
        }

        [Test]
        public void AndValidationFails()
        {
            var validationException = new ValidationException("Exception");
            Mediator.Setup(m => m.SendAsync(It.IsAny<GetAgreementEventsRequest>())).ThrowsAsync(validationException);

            Assert.ThrowsAsync<ValidationException>(() => Orchestrator.GetEvents(DateTime.Now.AddDays(-30).ToString("yyyyMMddHHmmss"), DateTime.Now.ToString("yyyyMMddHHmmss"), 100, 1, 123));

            EventsLogger.Verify(x => x.Warn(validationException, "Invalid request", null, null, null));
        }

        [Test]
        public void AndAnExceptionOccursThenTheErrorIsLogged()
        {
            var exception = new Exception("Exception");
            Mediator.Setup(m => m.SendAsync(It.IsAny<GetAgreementEventsRequest>())).ThrowsAsync(exception);

            Assert.ThrowsAsync<Exception>(() => Orchestrator.GetEvents(DateTime.Now.AddDays(-30).ToString("yyyyMMddHHmmss"), DateTime.Now.ToString("yyyyMMddHHmmss"), 100, 1, 123));

            EventsLogger.Verify(x => x.Error(exception, exception.Message, null, null, null));
        }

        private static bool RequestMatchesParameters(GetAgreementEventsRequest x, DateTime toDateTime, DateTime fromDateTime, int fromEventId, int pageNumber, int pageSize)
        {
            return x.ToDateTime.ToString("yyyyMMddHHmmss") == toDateTime.ToString("yyyyMMddHHmmss") &&
                   x.FromDateTime.ToString("yyyyMMddHHmmss") == fromDateTime.ToString("yyyyMMddHHmmss") &&
                   x.FromEventId == fromEventId &&
                   x.PageNumber == pageNumber && 
                   x.PageSize == pageSize;
        }
    }
}
