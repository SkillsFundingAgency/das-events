CREATE PROCEDURE [dbo].[CreateApprenticeshipEvents]
	@events [dbo].[ApprenticeshipEventsType] READONLY,
	@priceHistory [dbo].[PriceHistoryType]  READONLY

AS
	DECLARE @generated_keys table([Id] bigint, apprenticeshipId bigint)
	
	INSERT INTO [dbo].[ApprenticeshipEvents] (
		[Event], 
		[CreatedOn], 
		[ApprenticeshipId],
		[PaymentOrder], 
		[PaymentStatus],
		[AgreementStatus],
		[ProviderId], 
		[LearnerId], 
		[EmployerAccountId],
		[TrainingType], 
		[TrainingId], 
		[TrainingStartDate],
		[TrainingEndDate], 
		[TrainingTotalCost],
		[LegalEntityId],
		[LegalEntityName],
		[LegalEntityOrganisationType],
		[EffectiveFrom], 
		[EffectiveTo], 
		[DateOfBirth],
		[TransferSenderId],
		[TransferSenderName],
		[TransferApprovalStatus],
		[TransferApprovalActionedOn],
		[StoppedOnDate],
		[PausedOnDate],
		[AccountLegalEntityPublicHashedId]
	)
	OUTPUT inserted.Id, inserted.ApprenticeshipId INTO @generated_keys
	SELECT
		[Event], 
		[CreatedOn], 
		[ApprenticeshipId],
		[PaymentOrder], 
		[PaymentStatus],
		[AgreementStatus],
		[ProviderId], 
		[LearnerId], 
		[EmployerAccountId],
		[TrainingType], 
		[TrainingId], 
		[TrainingStartDate],
		[TrainingEndDate], 
		[TrainingTotalCost],
		[LegalEntityId],
		[LegalEntityName],
		[LegalEntityOrganisationType],
		[EffectiveFrom], 
		[EffectiveTo], 
		[DateOfBirth],
		[TransferSenderId],
		[TransferSenderName],
		[TransferApprovalStatus],
		[TransferApprovalActionedOn],
		[StoppedOnDate],
		[PausedOnDate],
		null
	FROM @events

	INSERT INTO [dbo].PriceHistory
	(ApprenticeshipEventsId,TotalCost,EffectiveFrom,EffectiveTo)
	SELECT k.Id, ph.TotalCost, ph.EffectiveFrom, ph.EffectiveTo FROM
	@priceHistory as ph
	LEFT JOIN
	@generated_keys as k
	ON ph.ApprenticeshipId = k.apprenticeshipId