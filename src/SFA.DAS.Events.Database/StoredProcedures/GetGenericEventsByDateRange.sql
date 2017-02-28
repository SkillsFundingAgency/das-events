CREATE PROCEDURE [dbo].[GetGenericEventsByDateRange]
	@eventTypes [dbo].[eventType] Readonly,	
	@fromDate Datetime,
	@toDate Datetime,
	@offset int,
	@pageSize int
AS
	SELECT * FROM GenericEvents ge
	INNER JOIN  @eventTypes et ON ge.[Type] = et.Name
	WHERE CreatedOn >=  @fromDate AND CreatedOn < @toDate
	ORDER BY CreatedOn OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY