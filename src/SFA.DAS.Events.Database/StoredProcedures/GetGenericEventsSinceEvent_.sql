CREATE PROCEDURE [dbo].[GetGenericEventsSinceEvent]
	@eventTypes [dbo].[eventType] Readonly,	
	@fromEventId int,
	@offset int,
	@pageSize int
AS
	SELECT * FROM GenericEvents ge
	INNER JOIN  @eventTypes et ON ge.Type = et.Name
	WHERE Id >= @fromEventId	
	ORDER BY CreatedOn OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY

