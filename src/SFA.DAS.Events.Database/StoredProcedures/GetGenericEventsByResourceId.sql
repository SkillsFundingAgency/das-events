CREATE PROCEDURE [dbo].[GetGenericEventsByResourceId]
	@resourceType NVARCHAR(255),	
	@resourceId NVARCHAR(255),	
	@fromDate Datetime = NULL,
	@toDate Datetime = NULL,
	@offset int,
	@pageSize int
AS
	SELECT * FROM GenericEvents ge
	WHERE ResourceType = @resourceType AND ResourceId = @resourceId AND (@fromDate IS NULL OR CreatedOn >=  @fromDate) AND (@toDate IS NULL OR CreatedOn < @toDate)
	ORDER BY CreatedOn OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY