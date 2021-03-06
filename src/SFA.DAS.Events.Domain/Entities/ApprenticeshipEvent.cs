﻿using System;
using System.Collections.Generic;

namespace SFA.DAS.Events.Domain.Entities
{
    public class ApprenticeshipEvent : BaseEvent
    {
        public string Event { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public DateTime? PausedOnDate { get; set; }
        public DateTime? StoppedOnDate { get; set; }
        public long ApprenticeshipId { get; set; }
        public AgreementStatus AgreementStatus { get; set; }
        public string ProviderId { get; set; }
        public string LearnerId { get; set; }
        public string EmployerAccountId { get; set; }
        public TrainingTypes TrainingType { get; set; }
        public string TrainingId { get; set; }
        public DateTime? TrainingStartDate { get; set; }
        public DateTime? TrainingEndDate { get; set; }
        public decimal? TrainingTotalCost { get; set; }
        public int PaymentOrder { get; set; }
        public string LegalEntityId { get; set; }
        public string LegalEntityName { get; set; }
        public string LegalEntityOrganisationType { get; set; }
        public string AccountLegalEntityPublicHashedId { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public IList<PriceHistory> PriceHistory { get; set; }
        public long? TransferSenderId { get; set; }
        public string TransferSenderName { get; set; }
        public TransferApprovalStatus? TransferApprovalStatus { get; set; }
        public DateTime? TransferApprovalActionedOn { get; set; }
    }
}
