CREATE PROCEDURE [dbo].[CreateGenericEvent]
	@eventType varchar(255),
	@eventPayload text,
	@resourceType NVARCHAR(255) = NULL,
	@resourceId NVARCHAR(255) = NULL
AS
	INSERT INTO [dbo].[GenericEvents] (Type, Payload, CreatedOn, ResourceType, ResourceId) 
	VALUES (@eventType, @eventPayload, GETUTCDATE(), @resourceType, @resourceId)
