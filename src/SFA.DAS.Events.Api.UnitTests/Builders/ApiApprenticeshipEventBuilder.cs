using System;
using System.Collections.Generic;
using SFA.DAS.Events.Api.Types;

namespace SFA.DAS.Events.Api.UnitTests.Builders
{
    internal class ApiApprenticeshipEventBuilder
    {
        private string _employerAccountId = "ABC123";
        private string _event = "Event 1";
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
        private DateTime? _pausedOnDate = DateTime.Today;
        private DateTime? _stoppedOnDate = DateTime.Today.AddDays(1);
        private int _apprenticeshipId = 12345;
        private string _legalEntityId = "LEID";
        private string _legalEntityName = "legal entity name";
        private string _legalEntityOrganisationType = "le type";
        private DateTime? _dateOfBirth = DateTime.Now.AddYears(-18);
        private List<PriceHistory> _priceHistory = new List<PriceHistory>();
        private long? _transferSenderId = 123;
        private string _transferSenderName = "Transfer Sender";
        private TransferApprovalStatus _transferSenderApprovalStatus = TransferApprovalStatus.TransferApproved;

        internal ApprenticeshipEvent Build()
        {
            return new ApprenticeshipEvent
            {
                EmployerAccountId = _employerAccountId,
                Event = _event,
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
                PausedOnDate = _pausedOnDate,
                StoppedOnDate = _stoppedOnDate,
                ApprenticeshipId = _apprenticeshipId,
                LegalEntityId = _legalEntityId,
                LegalEntityName = _legalEntityName,
                LegalEntityOrganisationType = _legalEntityOrganisationType,
                DateOfBirth = _dateOfBirth,
                PriceHistory = _priceHistory,
                TransferSenderId = _transferSenderId,
                TransferSenderName = _transferSenderName,
                TransferApprovalStatus = _transferSenderApprovalStatus
            };
        }
    }
}
