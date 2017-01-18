using System;
using System.Threading.Tasks;
using FluentValidation;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Api.Types;
using SFA.DAS.Events.Application.Commands.CreateAccountEvent;

namespace SFA.DAS.Events.Api.UnitTests.Orchestrators.AccountEventsOrchestratorTests
{
    [TestFixture]
    public class WhenICreateAnEvent : AccountEventsOrchestratorTestBase
    {
        [Test]
        public async Task ThenTheEventIsCreated()
        {
            var request = new AccountEvent { EmployerAccountId = "ABC123", Event = "Test" };
            await Orchestrator.CreateEvent(request);

            EventsLogger.Verify(x => x.Info($"Creating Account Event ({request.Event})", request.EmployerAccountId, null, request.Event));
            Mediator.Verify(m => m.SendAsync(It.Is<CreateAccountEventCommand>(x => x.EmployerAccountId == request.EmployerAccountId && x.Event == request.Event)));
        }

        [Test]
        public async Task AndValidationFailsThenTheFailureIsLogged()
        {
            var request = new AccountEvent();
            var validationException = new ValidationException("Exception");
            Mediator.Setup(m => m.SendAsync(It.Is<CreateAccountEventCommand>(x => x.EmployerAccountId == request.EmployerAccountId && x.Event == request.Event))).ThrowsAsync(validationException);

            Assert.ThrowsAsync<ValidationException>(() => Orchestrator.CreateEvent(request));

            EventsLogger.Verify(x => x.Warn(validationException, "Invalid request", request.EmployerAccountId, null, request.Event));
        }

        [Test]
        public async Task AndAnExceptionOccursThenTheErrorIsLogged()
        {
            var request = new AccountEvent();
            var exception = new Exception("Exception");
            Mediator.Setup(m => m.SendAsync(It.Is<CreateAccountEventCommand>(x => x.EmployerAccountId == request.EmployerAccountId && x.Event == request.Event))).ThrowsAsync(exception);

            Assert.ThrowsAsync<Exception>(() => Orchestrator.CreateEvent(request));

            EventsLogger.Verify(x => x.Error(exception, exception.Message, null, null, null));
        }
    }
}
