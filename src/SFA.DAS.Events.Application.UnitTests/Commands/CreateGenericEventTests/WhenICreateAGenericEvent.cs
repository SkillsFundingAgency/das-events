using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.Commands.CreateGenericEvent;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.UnitTests.Commands.CreateGenericEventTests
{
    [TestFixture]
    public class WhenICreateAGenericEvent : CreateGenericEventTestBase
    {
        [Test]
        public async Task ThenTheEventIsCreated()
        {
            var command = new CreateGenericEventCommand { Payload = "dfljihldfkmgfdg", Type = "EventType" };

            await Handler.Handle(command);

            EventsLogger.Verify(x => x.Info($"Creating Generic Event of type {command.Type}", null, null, null), Times.Once);
            Repository.Verify(x => x.Create(It.Is<GenericEvent>(e => e.Payload == command.Payload && e.Type == command.Type)));
        }

        [Test]
        public async Task AndTheEventCreationFailsThenTheExceptionIsLogged()
        {
            var command = new CreateGenericEventCommand { Payload = "dfljihldfkmgfdg", Type = "EventType" };
            var expectedException = new Exception("Test");

            Repository.Setup(x => x.Create(It.Is<GenericEvent>(e => e.Payload == command.Payload && e.Type == command.Type))).Throws(expectedException);

            Assert.ThrowsAsync<Exception>(() => Handler.Handle(command));

            EventsLogger.Verify(x => x.Error(expectedException, "Error storing generic event in database", null, null, null), Times.Once);
        }
    }
}
