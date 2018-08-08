using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Api.Types;
using SFA.DAS.Events.Api.UnitTests.Builders;
using SFA.DAS.Events.Application.Commands.BulkUploadCreateApprenticeshipEvents;

namespace SFA.DAS.Events.Api.UnitTests.Orchestrators.ApprenticeshipEventsOrchestratorTests
{
    [TestFixture]
    public class WhenICreateAListOfEvents : ApprenticeshipEventsOrchestratorTestBase
    {

        private List<ApprenticeshipEvent> _events;

        [SetUp]
        public void Init()
        {
            _events = new List<ApprenticeshipEvent>
            {
                new ApiApprenticeshipEventBuilder().WithPausedOnDate(null).WithStoppededOnDate(null).WithPriceHistory(null).Build(),
                new ApiApprenticeshipEventBuilder().WithPausedOnDate(null).Build(),
                new ApiApprenticeshipEventBuilder().WithStoppededOnDate(null).Build(),
                new ApiApprenticeshipEventBuilder().WithPriceHistory(new List<PriceHistory>
                {
                    new PriceHistory { EffectiveFrom = DateTime.Today, EffectiveTo = DateTime.Today.AddYears(1), TotalCost = 1000}
                }).Build()
            };

        }

        [Test]
        public async Task ThenTheEventsAreCreated()
        {
            await Orchestrator.CreateEvents(_events);

            EventsLogger.Verify(x => x.Info($"Bulk Uploading {_events.Count} Apprenticeship Event", null, null, null));
            Mediator.Verify(m => m.SendAsync(It.Is<BulkUploadCreateApprenticeshipEventsCommand>(x => BulkCommandMatchesEvents(x, _events))), Times.Once);
        }

        [Test]
        public void AndValidationFailsThenTheFailureIsLogged()
        {
            var validationException = new ValidationException("Exception");
            Mediator.Setup(m => m.SendAsync(It.IsAny<BulkUploadCreateApprenticeshipEventsCommand>())).ThrowsAsync(validationException);

            Assert.ThrowsAsync<ValidationException>(() => Orchestrator.CreateEvents(_events));

            EventsLogger.Verify(x => x.Warn(validationException, "Invalid apprenticeship event bulk upload request", null, null, null));
        }

        [Test]
        public void AndAnExceptionOccursThenTheErrorIsLogged()
        {
            var exception = new Exception("Exception");
            Mediator.Setup(m => m.SendAsync(It.IsAny<BulkUploadCreateApprenticeshipEventsCommand>())).ThrowsAsync(exception);

            Assert.ThrowsAsync<Exception>(() => Orchestrator.CreateEvents(_events));

            EventsLogger.Verify(x => x.Error(exception, exception.Message, null, null, null));
        }

        private bool BulkCommandMatchesEvents(BulkUploadCreateApprenticeshipEventsCommand command, List<ApprenticeshipEvent> events)
        {
            if (command.ApprenticeshipEvents?.Count != events?.Count)
            {
                return false;
            }

            var i = 0;
            foreach (var domainEvent in command.ApprenticeshipEvents)
            {
                if (!DomainEventMatchesApiEvent(domainEvent, events[i]))
                {
                    return false;
                }
                i++;
            }
            return true;
        }

        private bool DomainEventMatchesApiEvent(Domain.Entities.ApprenticeshipEvent domainEvent,
            ApprenticeshipEvent apiEvent)
        {

            return domainEvent.ProviderId == apiEvent.ProviderId &&
                   domainEvent.AgreementStatus.ToString() == apiEvent.AgreementStatus.ToString() &&
                   domainEvent.ApprenticeshipId == apiEvent.ApprenticeshipId &&
                   domainEvent.EmployerAccountId == apiEvent.EmployerAccountId &&
                   domainEvent.Event == apiEvent.Event &&
                   domainEvent.LearnerId == apiEvent.LearnerId &&
                   domainEvent.PaymentOrder == apiEvent.PaymentOrder &&
                   domainEvent.PaymentStatus.ToString() == apiEvent.PaymentStatus.ToString() &&
                   domainEvent.PausedOnDate == apiEvent.PausedOnDate &&
                   domainEvent.StoppedOnDate == apiEvent.StoppedOnDate &&
                   domainEvent.TrainingEndDate == apiEvent.TrainingEndDate &&
                   domainEvent.TrainingId == apiEvent.TrainingId &&
                   domainEvent.TrainingStartDate == apiEvent.TrainingStartDate &&
                   domainEvent.TrainingTotalCost == apiEvent.TrainingTotalCost &&
                   domainEvent.TrainingType.ToString() == apiEvent.TrainingType.ToString() &&
                   domainEvent.LegalEntityId == apiEvent.LegalEntityId &&
                   domainEvent.LegalEntityName == apiEvent.LegalEntityName &&
                   domainEvent.LegalEntityOrganisationType == apiEvent.LegalEntityOrganisationType &&
                   domainEvent.DateOfBirth == apiEvent.DateOfBirth &&
                   domainEvent.TransferSenderId == apiEvent.TransferSenderId &&
                   domainEvent.TransferSenderName == apiEvent.TransferSenderName &&
                   domainEvent.TransferApprovalStatus.ToString() == apiEvent.TransferApprovalStatus.ToString() &&
                   domainEvent.TransferApprovalActionedOn == apiEvent.TransferApprovalActionedOn &&
                   DomainPriceHistoryMatchesApiPriceHistory(domainEvent.PriceHistory, apiEvent.PriceHistory?.ToList());
        }
        private bool DomainPriceHistoryMatchesApiPriceHistory(IList<Domain.Entities.PriceHistory> domainPriceHistory,
            IList<PriceHistory> apiPriceHistory)
        {
            if (domainPriceHistory == null && apiPriceHistory == null)
            {
                return true;
            }

            if (domainPriceHistory?.Count != (apiPriceHistory?.Count ?? 0))
            {
                return false;
            }

            if (domainPriceHistory.Count == 0)
            {
                return true;
            }

            var i = 0;
            foreach (var priceHistory in domainPriceHistory)
            {
                if (priceHistory.EffectiveFrom != apiPriceHistory[i].EffectiveFrom ||
                    priceHistory.EffectiveTo != apiPriceHistory[i].EffectiveTo ||
                    priceHistory.TotalCost != apiPriceHistory[i].TotalCost)
                {
                    return false;
                }
                i++;
            }

            return true;
        }
    }
}

