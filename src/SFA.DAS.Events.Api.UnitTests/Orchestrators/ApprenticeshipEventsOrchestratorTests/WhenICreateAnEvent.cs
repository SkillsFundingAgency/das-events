using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Api.Types;
using SFA.DAS.Events.Api.UnitTests.Builders;
using SFA.DAS.Events.Application.Commands.CreateApprenticeshipEvent;

namespace SFA.DAS.Events.Api.UnitTests.Orchestrators.ApprenticeshipEventsOrchestratorTests
{
    [TestFixture]
    public class WhenICreateAnEvent : ApprenticeshipEventsOrchestratorTestBase
    {
        [Test]
        public async Task ThenTheEventIsCreated()
        {
            var request = new ApiApprenticeshipEventBuilder().Build();
            await Orchestrator.CreateEvent(request);

            EventsLogger.Verify(x => x.Info($"Creating Apprenticeship Event ({request.Event}) for Employer: {request.EmployerAccountId}, Provider: {request.ProviderId}", request.EmployerAccountId, request.ProviderId, request.Event));
            Mediator.Verify(m => m.SendAsync(It.Is<CreateApprenticeshipEventCommand>(x => CommandMatchesRequest(x, request))), Times.Once);
        }

        [Test]
        public void AndValidationFailsThenTheFailureIsLogged()
        {
            var request = new ApprenticeshipEvent { PriceHistory = new List<PriceHistory>() };
            var validationException = new ValidationException("Exception");
            Mediator.Setup(m => m.SendAsync(It.IsAny<CreateApprenticeshipEventCommand>())).ThrowsAsync(validationException);

            Assert.ThrowsAsync<ValidationException>(() => Orchestrator.CreateEvent(request));

            EventsLogger.Verify(x => x.Warn(validationException, "Invalid request", request.EmployerAccountId, request.ProviderId, request.Event));
        }

        [Test]
        public void AndAnExceptionOccursThenTheErrorIsLogged()
        {
            var request = new ApprenticeshipEvent { PriceHistory = new List<PriceHistory>() };
            var exception = new Exception("Exception");
            Mediator.Setup(m => m.SendAsync(It.IsAny<CreateApprenticeshipEventCommand>())).ThrowsAsync(exception);

            Assert.ThrowsAsync<Exception>(() => Orchestrator.CreateEvent(request));

            EventsLogger.Verify(x => x.Error(exception, exception.Message, null, null, null));
        }

        private bool CommandMatchesRequest(CreateApprenticeshipEventCommand command, ApprenticeshipEvent request)
        {
            return command.ProviderId == request.ProviderId &&
                   command.AgreementStatus.ToString() == request.AgreementStatus.ToString() &&
                   command.ApprenticeshipId == request.ApprenticeshipId &&
                   command.EmployerAccountId == request.EmployerAccountId &&
                   command.Event == request.Event &&
                   command.LearnerId == request.LearnerId &&
                   command.PaymentOrder == request.PaymentOrder &&
                   command.PaymentStatus.ToString() == request.PaymentStatus.ToString() &&
                   command.TrainingEndDate == request.TrainingEndDate &&
                   command.TrainingId == request.TrainingId &&
                   command.TrainingStartDate == request.TrainingStartDate &&
                   command.TrainingTotalCost == request.TrainingTotalCost &&
                   command.TrainingType.ToString() == request.TrainingType.ToString() &&
                   command.LegalEntityId == request.LegalEntityId &&
                   command.LegalEntityName == request.LegalEntityName &&
                   command.LegalEntityOrganisationType == request.LegalEntityOrganisationType &&
                   command.DateOfBirth == request.DateOfBirth;
        }
    }
}
