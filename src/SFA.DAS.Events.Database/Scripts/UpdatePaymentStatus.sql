-- UPDATE PaymentStatus
BEGIN TRANSACTION [Tran1]
BEGIN TRY


UPDATE [SFA.DAS.Events.Database].[dbo].[ApprenticeshipEvents]
SET [PaymentStatus]='0'
WHERE [PaymentStatus]='PendingApproval'

UPDATE [SFA.DAS.Events.Database].[dbo].[ApprenticeshipEvents]
SET [PaymentStatus]='1'
WHERE [PaymentStatus]='Active'

UPDATE [SFA.DAS.Events.Database].[dbo].[ApprenticeshipEvents]
SET [PaymentStatus]='2'
WHERE [PaymentStatus]='Paused'

UPDATE [SFA.DAS.Events.Database].[dbo].[ApprenticeshipEvents]
SET [PaymentStatus]='3'
WHERE [PaymentStatus]='Cancelled'

UPDATE [SFA.DAS.Events.Database].[dbo].[ApprenticeshipEvents]
SET [PaymentStatus]='4'
WHERE [PaymentStatus]='Completed'

UPDATE [SFA.DAS.Events.Database].[dbo].[ApprenticeshipEvents]
SET [PaymentStatus]='5'
WHERE [PaymentStatus]='Deleted'

ALTER TABLE [SFA.DAS.Events.Database].[dbo].[ApprenticeshipEvents] ALTER COLUMN PaymentStatus SMALLINT NOT NULL

