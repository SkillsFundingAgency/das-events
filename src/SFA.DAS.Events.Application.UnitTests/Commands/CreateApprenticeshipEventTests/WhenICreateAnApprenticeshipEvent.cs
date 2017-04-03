using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.Commands.CreateApprenticeshipEvent;
using SFA.DAS.Events.Application.UnitTests.Builders;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.UnitTests.Commands.CreateApprenticeshipEventTests
{
    [TestFixture]
    public class WhenICreateAnApprenticeshipEvent : CreateApprenticeshipEventTestBase
    {
        [Test]
        public async Task ThenTheEventIsCreated()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().Build();

            await Handler.Handle(command);

            EventsLogger.Verify(x => x.Info($"Received message {command.Event}", command.EmployerAccountId, command.ProviderId, command.Event), Times.Once);
            Repository.Verify(x => x.Create(It.Is<ApprenticeshipEvent>(e => EventMatchesCommand(e, command))));
            EventsLogger.Verify(x => x.Info($"Finished processing message {command.Event}", command.EmployerAccountId, command.ProviderId, command.Event), Times.Once);
        }

        [Test]
        public async Task AndTheEventCreationFailsThenTheExceptionIsLogged()
        {
            var command = new CreateApprenticeshipEventCommandBuilder().Build();
            var expectedException = new Exception("Test");

            Repository.Setup(x => x.Create(It.Is<ApprenticeshipEvent>(e => EventMatchesCommand(e, command)))).Throws(expectedException);

            Assert.ThrowsAsync<Exception>(() => Handler.Handle(command));

            EventsLogger.Verify(x => x.Error(expectedException, $"Error processing message {command.Event} - {expectedException.Message}", command.EmployerAccountId, command.ProviderId, command.Event), Times.Once);
        }

        private static bool EventMatchesCommand(ApprenticeshipEvent e, CreateApprenticeshipEventCommand command)
        {
            return e.EmployerAccountId == command.EmployerAccountId &&
                   e.Event == command.Event &&
                   e.ProviderId == command.ProviderId &&
                   e.AgreementStatus == command.AgreementStatus &&
                   e.ApprenticeshipId == command.ApprenticeshipId &&
                   e.LearnerId == command.LearnerId &&
                   e.PaymentOrder == command.PaymentOrder &&
                   e.PaymentStatus == command.PaymentStatus &&
                   e.TrainingEndDate == command.TrainingEndDate &&
                   e.TrainingId == command.TrainingId &&
                   e.TrainingStartDate == command.TrainingStartDate &&
                   e.TrainingTotalCost == command.TrainingTotalCost &&
                   e.TrainingType == command.TrainingType &&
                   e.LegalEntityId == command.LegalEntityId &&
                   e.LegalEntityName == command.LegalEntityName &&
                   e.LegalEntityOrganisationType == command.LegalEntityOrganisationType;
        }
    }
}
