using System;
using SFA.DAS.Events.Application.Commands.CreateApprenticeshipEvent;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.UnitTests.Builders
{
    internal class CreateApprenticeshipEventCommandBuilder
    {
        private AgreementStatus _agreementStatus = AgreementStatus.BothAgreed;
        private int _apprenticeshipId = 32489;
        private string _providerId = "ABC123";
        private string _employerAccountId = "ZZZ999";
        private string _event = "Test";
        private string _learnerId = "LRNID";
        private int _paymentOrder = 3;
        private PaymentStatus _paymentStatus = PaymentStatus.Active;
        private DateTime _trainingEndDate = DateTime.Now.AddYears(1);
        private string _trainingId = "TRNID";
        private DateTime _trainingStartDate = DateTime.Now.AddDays(-30);
        private decimal _trainingTotalCost = 12345.67m;
        private TrainingTypes _trainingTypes = TrainingTypes.Framework;

        internal CreateApprenticeshipEventCommand Build()
        {
            return new CreateApprenticeshipEventCommand
            {
                AgreementStatus = _agreementStatus,
                ApprenticeshipId = _apprenticeshipId,
                ProviderId = _providerId,
                EmployerAccountId = _employerAccountId,
                Event = _event,
                LearnerId = _learnerId,
                PaymentOrder = _paymentOrder,
                PaymentStatus = _paymentStatus,
                TrainingEndDate = _trainingEndDate,
                TrainingId = _trainingId,
                TrainingStartDate = _trainingStartDate,
                TrainingTotalCost = _trainingTotalCost,
                TrainingType = _trainingTypes
            };
        }

        internal CreateApprenticeshipEventCommandBuilder WithEvent(string @event)
        {
            _event = @event;
            return this;
        }

        internal CreateApprenticeshipEventCommandBuilder WithApprenticeshipId(int apprenticeshipId)
        {
            _apprenticeshipId = apprenticeshipId;
            return this;
        }

        internal CreateApprenticeshipEventCommandBuilder WithProviderId(string providerId)
        {
            _providerId = providerId;
            return this;
        }

        internal CreateApprenticeshipEventCommandBuilder WithLearnerId(string learnerId)
        {
            _learnerId = learnerId;
            return this;
        }

        internal CreateApprenticeshipEventCommandBuilder WithEmployerAccountId(string employerAccountId)
        {
            _employerAccountId = employerAccountId;
            return this;
        }

        internal CreateApprenticeshipEventCommandBuilder WithTrainingId(string trainingId)
        {
            _trainingId = trainingId;
            return this;
        }

        internal CreateApprenticeshipEventCommandBuilder WithTrainingStartDate(DateTime trainingStartDate)
        {
            _trainingStartDate = trainingStartDate;
            return this;
        }

        internal CreateApprenticeshipEventCommandBuilder WithTrainingEndDate(DateTime trainingEndDate)
        {
            _trainingEndDate = trainingEndDate;
            return this;
        }

        internal CreateApprenticeshipEventCommandBuilder WithTrainingTotalCost(decimal trainingTotalCost)
        {
            _trainingTotalCost = trainingTotalCost;
            return this;
        }
    }
}
