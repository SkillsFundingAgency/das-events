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
        public void AndEventIsNotProvidedThenValidationFails()
        {
            var command = new CreateAgreementEventCommand { ContractType = "MainProvider", ProviderId = "ZZZ999" };

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public void AndProviderIdIsNotProvidedThenValidationFails()
        {

            var command = new CreateAgreementEventCommand { ContractType = "MainProvider", Event = "ABC" };

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }
    }
}
