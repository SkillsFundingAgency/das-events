CREATE PROCEDURE [dbo].[CreateGenericEvent]
	@eventType varchar(255),
	@eventPayload text,
	@resourceUri NVARCHAR(1000) = NULL,
	@resourceType NVARCHAR(255) = NULL,
	@resourceId NVARCHAR(255) = NULL
AS
	INSERT INTO [dbo].[GenericEvents] (Type, Payload, CreatedOn, ResourceUri, ResourceType, ResourceId) 
	VALUES (@eventType, @eventPayload, GETUTCDATE(), @resourceUri, @resourceType, @resourceId)
