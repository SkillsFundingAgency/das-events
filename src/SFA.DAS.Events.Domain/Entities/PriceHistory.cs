using System;

namespace SFA.DAS.Events.Domain.Entities
{
    public class PriceHistory
    {
        public long ApprenticeshipEventsId { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
    }
}