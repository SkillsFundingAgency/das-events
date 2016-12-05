BEGIN TRANSACTION [Tran1]
BEGIN TRY


UPDATE [dbo].[ApprenticeshipEvents]
SET [PaymentStatus]='0'
WHERE [PaymentStatus]='PendingApproval'

UPDATE [dbo].[ApprenticeshipEvents]
SET [PaymentStatus]='1'
WHERE [PaymentStatus]='Active'

UPDATE [dbo].[ApprenticeshipEvents]
SET [PaymentStatus]='2'
WHERE [PaymentStatus]='Paused'

UPDATE [dbo].[ApprenticeshipEvents]
SET [PaymentStatus]='3'
WHERE [PaymentStatus]='Cancelled'

UPDATE [dbo].[ApprenticeshipEvents]
SET [PaymentStatus]='4'
WHERE [PaymentStatus]='Completed'

UPDATE [dbo].[ApprenticeshipEvents]
SET [PaymentStatus]='5'
WHERE [PaymentStatus]='Deleted'

ALTER TABLE [dbo].[ApprenticeshipEvents] ALTER COLUMN PaymentStatus SMALLINT NOT NULL

COMMIT TRANSACTION [Tran1]
END TRY
BEGIN CATCH
  ROLLBACK TRANSACTION [Tran1]
END CATCH  

GO