using System;
using System.Threading.Tasks;
using FluentValidation;
using NUnit.Framework;
using SFA.DAS.Events.Application.UnitTests.Builders;

namespace SFA.DAS.Events.Application.UnitTests.CreateApprenticeshipEventTests
{
    [TestFixture]
    public class WhenIValidateTheCommand : CreateApprenticeshipEventTestBase
    {
        [Test]
        public async Task AndEventIsNotProvided()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().WithEvent(null).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public async Task AndApprenticeshipIdIsNotProvided()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().WithApprenticeshipId(0).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public async Task AndProviderIdIsNotProvided()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().WithProviderId(null).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public async Task AndLearnerIdIsNotProvided()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().WithLearnerId(null).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public async Task AndEmployerAccountIdIsNotProvided()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().WithEmployerAccountId(null).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public async Task AndTrainingIdIsNotProvided()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().WithTrainingId(null).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public async Task AndTrainingStartDateIsAfterTheEndDate()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().WithTrainingStartDate(DateTime.Now.AddDays(-1)).WithTrainingEndDate(DateTime.Now.AddDays(-2)).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }

        [Test]
        public async Task AndTrainingTotalCostIsNotProvided()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().WithTrainingTotalCost(-1).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(command));
        }
    }
}
