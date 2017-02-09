using System;
using System.Threading.Tasks;
using FluentValidation;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Api.Types;
using SFA.DAS.Events.Application.Commands.CreateAgreementEvent;

namespace SFA.DAS.Events.Api.UnitTests.Orchestrators.AgreementEventsOrchestratorTests
{
    [TestFixture]
    public class WhenICreateAnEvent : AgreementEventsOrchestratorTestBase
    {
        [Test]
        public async Task ThenTheEventIsCreated()
        {
            var request = new AgreementEvent { ContractType = "MainProvider", Event = "Test", ProviderId = "ZZZ999" };
            await Orchestrator.CreateEvent(request);

            EventsLogger.Verify(x => x.Info($"Creating Agreement Event ({request.Event}), Contract: {request.ContractType}, Provider: {request.ProviderId}", null, request.ProviderId, request.Event));
            Mediator.Verify(m => m.SendAsync(It.Is<CreateAgreementEventCommand>(x => x.ContractType == request.ContractType && x.Event == request.Event && x.ProviderId == request.ProviderId)));
        }

        [Test]
        public async Task AndValidationFailsThenTheFailureIsLogged()
        {
            var request = new AgreementEvent();
            var validationException = new ValidationException("Exception");
            Mediator.Setup(m => m.SendAsync(It.IsAny<CreateAgreementEventCommand>())).ThrowsAsync(validationException);

            Assert.ThrowsAsync<ValidationException>(() => Orchestrator.CreateEvent(request));

            EventsLogger.Verify(x => x.Warn(validationException, "Invalid request", request.ContractType, request.ProviderId, request.Event));
        }

        [Test]
        public async Task AndAnExceptionOccursThenTheErrorIsLogged()
        {
            var request = new AgreementEvent();
            var exception = new Exception("Exception");
            Mediator.Setup(m => m.SendAsync(It.IsAny<CreateAgreementEventCommand>())).ThrowsAsync(exception);

            Assert.ThrowsAsync<Exception>(() => Orchestrator.CreateEvent(request));

            EventsLogger.Verify(x => x.Error(exception, exception.Message, null, null, null));
        }
    }
}
