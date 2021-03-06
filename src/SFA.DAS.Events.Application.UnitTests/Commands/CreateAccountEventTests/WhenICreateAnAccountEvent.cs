﻿using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.Commands.CreateAccountEvent;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.UnitTests.Commands.CreateAccountEventTests
{
    [TestFixture]
    public class WhenICreateAnAccountEvent : CreateAccountEventTestBase
    {
        [Test]
        public async Task ThenTheEventIsCreated()
        {
            var command = new CreateAccountEventCommand { ResourceUri = "/api/accounts/ABC123", Event = "Some Event" };

            await Handler.Handle(command);

            EventsLogger.Verify(x => x.Info($"Received message {command.Event}", command.ResourceUri, null, command.Event), Times.Once);
            Repository.Verify(x => x.Create(It.Is<AccountEvent>(e => e.ResourceUri == command.ResourceUri && e.Event == command.Event)));
            EventsLogger.Verify(x => x.Info($"Finished processing message {command.Event}", null, null, null), Times.Once);
        }

        [Test]
        public void AndTheEventCreationFailsThenTheExceptionIsLogged()
        {
            var command = new CreateAccountEventCommand { ResourceUri = "/api/accounts/ABC123", Event = "Some Event" };
            var expectedException = new Exception("Test");

            Repository.Setup(x => x.Create(It.Is<AccountEvent>(e => e.ResourceUri == command.ResourceUri && e.Event == command.Event))).Throws(expectedException);

            Assert.ThrowsAsync<Exception>(() => Handler.Handle(command));

            EventsLogger.Verify(x => x.Error(expectedException, $"Error processing message {command.Event} - {expectedException.Message}", command.ResourceUri, null, command.Event), Times.Once);
        }
    }
}
