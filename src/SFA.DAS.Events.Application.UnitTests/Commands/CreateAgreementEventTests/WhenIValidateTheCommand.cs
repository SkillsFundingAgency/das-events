using System.Threading.Tasks;
using FluentValidation;
using NUnit.Framework;
using SFA.DAS.Events.Application.Commands.CreateAgreementEvent;

namespace SFA.DAS.Events.Application.UnitTests.Commands.CreateAgreementEventTests
{
    [TestFixture]
    public class WhenIValidateTheCommand : CreateAgreementEventTestBase
    {
        [Test]
        public async Task AndEventIsNotProvidedThenValidationFails()
        {
            var command = new CreateAgreementEventCommand { EmployerAccountId = "ABC", ProviderId = "ZZZ999" };

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public async Task AndProviderIdIsNotProvidedThenValidationFails()
        {

            var command = new CreateAgreementEventCommand { EmployerAccountId = "ABC", Event = "ABC" };

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }
    }
}
