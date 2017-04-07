using System;
using System.Threading.Tasks;
using FluentValidation;
using NUnit.Framework;
using SFA.DAS.Events.Application.UnitTests.Builders;

namespace SFA.DAS.Events.Application.UnitTests.Commands.CreateApprenticeshipEventTests
{
    [TestFixture]
    public class WhenIValidateTheCommand : CreateApprenticeshipEventTestBase
    {
        [Test]
        public async Task AndEventIsNotProvidedThenValidationFails()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().WithEvent(null).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public async Task AndApprenticeshipIdIsNotProvidedThenValidationFails()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().WithApprenticeshipId(0).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public async Task AndLearnerIdIsNotProvidedThenValidationFails()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().WithLearnerId(null).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public async Task AndEmployerAccountIdIsNotProvidedThenValidationFails()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().WithEmployerAccountId(null).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public async Task AndTrainingStartDateIsAfterTheEndDateThenValidationFails()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().WithTrainingStartDate(DateTime.Now.AddDays(-1)).WithTrainingEndDate(DateTime.Now.AddDays(-2)).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public async Task AndLegalEntityIdIsNotProvidedThenValidationFails()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().WithLegalEntityId(null).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public async Task AndLegalEntityNameIsNotProvidedThenValidationFails()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().WithLegalEntityName(null).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public async Task AndLegalEntityOrganisationTypeIsNotProvidedThenValidationFails()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().WithLegalEntityOrganisationType(null).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }
    }
}
