CREATE PROCEDURE [dbo].[CreateGenericEvent]
	@event varchar(255),
	@eventType varchar(255),
	@eventPayload text
AS
	INSERT INTO [dbo].[GenericEvents] (Event, Type, Payload, CreatedOn) 
	VALUES (@event, @eventType, @eventPayload, GETDATE())
