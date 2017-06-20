CREATE TYPE [dbo].[PriceHistoryType] AS TABLE(
    [ApprenticeshipId] BIGINT NOT NULL,
	[TotalCost] DECIMAL NOT NULL,
	[EffectiveFrom] DateTime NOT NULL,
	[EffectiveTo] DateTime NULL
)
