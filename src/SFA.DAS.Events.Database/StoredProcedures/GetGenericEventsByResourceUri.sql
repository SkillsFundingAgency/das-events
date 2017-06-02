CREATE PROCEDURE [dbo].[GetGenericEventsByResourceUri]
	@resourceUri NVARCHAR(1000),	
	@fromDate Datetime = NULL,
	@toDate Datetime = NULL,
	@offset int,
	@pageSize int
AS
	SELECT * FROM GenericEvents ge
	WHERE ResourceUri = @resourceUri AND (@fromDate IS NULL OR CreatedOn >=  @fromDate) AND (@toDate IS NULL OR CreatedOn < @toDate)
	ORDER BY CreatedOn OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY