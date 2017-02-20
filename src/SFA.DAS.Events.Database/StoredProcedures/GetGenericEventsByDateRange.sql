CREATE PROCEDURE [dbo].[GetGenericEventsByDateRange]
	@eventTypes [dbo].[eventType] Readonly,	
	@fromDate Datetime NOT NULL,
	@toDate Datetime NOT NULL,
	@offset int NOT NULL,
	@pageSize int NOT NULL
AS
	SELECT * FROM GenericEvents ge
	INNER JOIN  @eventTypes et ON ge.[Type] = et.Name
	WHERE CreatedOn >=  @fromDate AND CreatedOn < @toDate
	ORDER BY CreatedOn OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY