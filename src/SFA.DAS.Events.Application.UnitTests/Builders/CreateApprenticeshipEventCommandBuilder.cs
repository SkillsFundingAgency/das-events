using System;
using System.Collections.Generic;

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
        private DateTime? _pausedOnDate = DateTime.Today;
        private DateTime? _stoppedOnDate = DateTime.Today.AddDays(1);
        private DateTime _trainingEndDate = DateTime.Now.AddYears(1);
        private string _trainingId = "TRNID";
        private DateTime _trainingStartDate = DateTime.Now.AddDays(-30);
        private decimal _trainingTotalCost = 12345.67m;
        private TrainingTypes _trainingTypes = TrainingTypes.Framework;
        private string _legalEntityId = "LEID";
        private string _legalEntityName = "legal entity name";
        private string _legalEntityOrganisationType = "le type";
        private string _accountLegalEntityPublicHashedId = "123456";
        private DateTime? _dateOfBirth = DateTime.Now.AddYears(-18);
        private IList<PriceHistory> _priceHistory = new List<PriceHistory> { new PriceHistory() };
        private long? _transferSenderId = 123;
        private string _transferSenderName = "Transfer Sender";
        private TransferApprovalStatus _transferApprovalStatus = TransferApprovalStatus.Pending;
        private DateTime? _transferApprovalActionedOn  = DateTime.Now;

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
                PausedOnDate = _pausedOnDate,
                StoppedOnDate = _stoppedOnDate,
                TrainingEndDate = _trainingEndDate,
                TrainingId = _trainingId,
                TrainingStartDate = _trainingStartDate,
                TrainingTotalCost = _trainingTotalCost,
                TrainingType = _trainingTypes,
                LegalEntityId = _legalEntityId,
                LegalEntityName = _legalEntityName,
                LegalEntityOrganisationType = _legalEntityOrganisationType,
                AccountLegalEntityPublicHashedId = _accountLegalEntityPublicHashedId,
                DateOfBirth = _dateOfBirth,
                PriceHistory = _priceHistory,
                TransferSenderId = _transferSenderId,
                TransferSenderName = _transferSenderName,
                TransferApprovalStatus = _transferApprovalStatus,
                TransferApprovalActionedOn  = _transferApprovalActionedOn
            };
        }
    }
}
