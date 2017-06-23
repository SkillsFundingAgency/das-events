using System.Threading.Tasks;
using FluentValidation;
using NUnit.Framework;
using SFA.DAS.Events.Application.Commands.CreateGenericEvent;

namespace SFA.DAS.Events.Application.UnitTests.Commands.CreateGenericEventTests
{
    [TestFixture]
    public class WhenIValidateTheCommand : CreateGenericEventTestBase
    {
        [Test]
        public void AndTypeIsNotProvidedThenValidationFails()
        {
            var command = new CreateGenericEventCommand { Payload = "dlfkjgndfgfd" };

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public void AndPayloadIsNotProvidedThenValidationFails()
        {

            var command = new CreateGenericEventCommand { Type = "ABC" };

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }
    }
}
