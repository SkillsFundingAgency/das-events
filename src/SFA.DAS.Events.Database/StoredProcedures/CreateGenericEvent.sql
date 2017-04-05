CREATE PROCEDURE [dbo].[CreateGenericEvent]
	@eventType varchar(255),
	@eventPayload text
AS
	INSERT INTO [dbo].[GenericEvents] (Type, Payload, CreatedOn) 
	VALUES (@eventType, @eventPayload, GETUTCDATE())
