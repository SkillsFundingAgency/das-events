using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.Commands.CreateAgreementEvent;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.UnitTests.Commands.CreateAgreementEventTests
{
    [TestFixture]
    public class WhenICreateAnAgreementEvent : CreateAgreementEventTestBase
    {
        [Test]
        public async Task ThenTheEventIsCreated()
        {
            var command = new CreateAgreementEventCommand { ContractType = "MainProvider", Event = "Some Event", ProviderId = "ZZZ999" };

            await Handler.Handle(command);

            EventsLogger.Verify(x => x.Info($"Received message {command.Event}", null, command.ProviderId, command.Event), Times.Once);
            Repository.Verify(x => x.Create(It.Is<AgreementEvent>(e => e.ContractType == command.ContractType && e.Event == command.Event && e.ProviderId == command.ProviderId)));
            EventsLogger.Verify(x => x.Info($"Finished processing message {command.Event}", null, null, null), Times.Once);
        }

        [Test]
        public void AndTheEventCreationFailsThenTheExceptionIsLogged()
        {
            var command = new CreateAgreementEventCommand { ContractType = "MainProvider", Event = "Some Event", ProviderId = "ZZZ999" };
            var expectedException = new Exception("Test");

            Repository.Setup(x => x.Create(It.Is<AgreementEvent>(e => e.ContractType == command.ContractType && e.Event == command.Event && e.ProviderId == command.ProviderId))).Throws(expectedException);

            Assert.ThrowsAsync<Exception>(() => Handler.Handle(command));

            EventsLogger.Verify(x => x.Error(expectedException, $"Error processing message {command.Event} - {expectedException.Message}", null, command.ProviderId, command.Event), Times.Once);
        }
    }
}
