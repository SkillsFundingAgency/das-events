using System.Threading.Tasks;
using FluentValidation;
using NUnit.Framework;
using SFA.DAS.Events.Application.Commands.CreateAccountEvent;

namespace SFA.DAS.Events.Application.UnitTests.CreateAccountEventTests
{
    [TestFixture]
    public class WhenIValidateTheCommand : CreateAccountEventTestBase
    {
        [Test]
        public async Task AndEventIsNotProvided()
        {
            var command = new CreateAccountEventCommand { EmployerAccountId = "ABC" };

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public async Task AndEmployerAccountIdIsNotProvided()
        {

            var command = new CreateAccountEventCommand { Event = "ABC" };

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }
    }
}
