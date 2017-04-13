CREATE PROCEDURE [dbo].[BulkUploadApprenticeshipEvents]
	@apprenticeshipEvents ApprenticeshipEventsTable READONLY
AS
	BEGIN
		SET NOCOUNT ON;
		
		BEGIN TRY
			BEGIN TRANSACTION

			INSERT INTO [dbo].[ApprenticeshipEvents]
			(
				Event,
				CreatedOn,
				ApprenticeshipId, 
				PaymentOrder,
				PaymentStatus, 
				AgreementStatus, 
				ProviderId, 
				LearnerId, 
				EmployerAccountId, 
				TrainingType, 
				TrainingId, 
				TrainingStartDate, 
				TrainingEndDate, 
				TrainingTotalCost,
				LegalEntityId, 
				LegalEntityName, 
				LegalEntityOrganisationType,
				EffectiveFrom,
				EffectiveTo,
				DateOfBirth)
			SELECT a.*
			FROM @apprenticeshipEvents a
			
			COMMIT TRANSACTION

		END TRY
		BEGIN CATCH

			DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE(),
				@ErrNum INT = ERROR_NUMBER(),
				@ErrProc NVARCHAR(126) = ERROR_PROCEDURE()
			DECLARE @DataError NVARCHAR(4000) = 'Error loading data to ApprenticeshipEvents table'
				+ CONVERT(NVARCHAR(10), @ErrNum) + ', Error Details: '
				+ @ErrMsg

			ROLLBACK TRANSACTION
			RAISERROR (@DataError, 16, 1)

		END CATCH
	END
GO
