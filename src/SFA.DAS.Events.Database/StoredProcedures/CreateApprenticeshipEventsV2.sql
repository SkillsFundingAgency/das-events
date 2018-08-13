CREATE PROCEDURE [dbo].[CreateApprenticeshipEventsV2]
	@events [dbo].[ApprenticeshipEventsTypeV2] READONLY,
	@priceHistory [dbo].[PriceHistoryType]  READONLY

AS
	DECLARE @generated_keys table([Id] bigint, apprenticeshipId bigint)
	
	INSERT INTO [dbo].[ApprenticeshipEvents]
	OUTPUT inserted.Id, inserted.ApprenticeshipId INTO @generated_keys
	SELECT * FROM @events

	INSERT INTO [dbo].PriceHistory
	(ApprenticeshipEventsId,TotalCost,EffectiveFrom,EffectiveTo)
	SELECT k.Id, ph.TotalCost, ph.EffectiveFrom, ph.EffectiveTo FROM
	@priceHistory as ph
	LEFT JOIN
	@generated_keys as k
	ON ph.ApprenticeshipId = k.apprenticeshipId