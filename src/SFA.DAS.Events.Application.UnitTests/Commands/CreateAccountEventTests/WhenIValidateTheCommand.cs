using System.Threading.Tasks;
using FluentValidation;
using NUnit.Framework;
using SFA.DAS.Events.Application.Commands.CreateAccountEvent;

namespace SFA.DAS.Events.Application.UnitTests.Commands.CreateAccountEventTests
{
    [TestFixture]
    public class WhenIValidateTheCommand : CreateAccountEventTestBase
    {
        [Test]
        public void AndEventIsNotProvidedThenValidationFails()
        {
            var command = new CreateAccountEventCommand { ResourceUri = "/api/accounts/ABC" };

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public void AndResourceUriIsNotProvidedThenValidationFails()
        {

            var command = new CreateAccountEventCommand { Event = "ABC" };

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }
    }
}
