using System;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Api.UnitTests.Builders
{
    internal class DomainApprenticeshipEventBuilder
    {
        private DateTime _createdOn = DateTime.Now.AddDays(-2);
        private string _employerAccountId = "ABC123";
        private string _event = "Event 1";
        private int _id = 23;
        private string _providerId = "ZZZ123";
        private AgreementStatus _agreementStatus = AgreementStatus.BothAgreed;
        private DateTime _trainingEndDate = DateTime.Now.AddYears(2);
        private TrainingTypes _trainingTypes = TrainingTypes.Framework;
        private decimal _trainingTotalCost = 12345.67m;
        private string _learnerId = "LRNID";
        private int _paymentOrder = 1;
        private string _trainingId = "ANBC435";
        private DateTime _trainingStartDate = DateTime.Now.AddYears(-1);
        private PaymentStatus _paymentStatus = PaymentStatus.Active;
        private int _apprenticeshipId = 12345;

        internal ApprenticeshipEvent Build()
        {
            return new ApprenticeshipEvent
            {
                CreatedOn = _createdOn,
                EmployerAccountId = _employerAccountId,
                Event = _event,
                Id = _id,
                ProviderId = _providerId,
                AgreementStatus = _agreementStatus,
                TrainingEndDate = _trainingEndDate,
                TrainingType = _trainingTypes,
                TrainingTotalCost = _trainingTotalCost,
                LearnerId = _learnerId,
                PaymentOrder = _paymentOrder,
                TrainingId = _trainingId,
                TrainingStartDate = _trainingStartDate,
                PaymentStatus = _paymentStatus,
                ApprenticeshipId = _apprenticeshipId
            };
        }

        internal DomainApprenticeshipEventBuilder WithCreatedOn(DateTime createdOn)
        {
            _createdOn = createdOn;
            return this;
        }

        internal DomainApprenticeshipEventBuilder WithEvent(string @event)
        {
            _event = @event;
            return this;
        }

        internal DomainApprenticeshipEventBuilder WithTrainingId(string trainingId)
        {
            _trainingId = trainingId;
            return this;
        }
    }
}
