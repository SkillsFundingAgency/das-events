IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'AccountEvents_Temp')
BEGIN
	SET IDENTITY_INSERT dbo.AccountEvents ON

    INSERT INTO dbo.AccountEvents
		([Id], [Event], CreatedOn, ResourceUri)
	SELECT [Id], 'AccountCreated', CreatedOn, '/api/accounts/' + EmployerAccountId
	FROM dbo.AccountEvents_Temp

	SET IDENTITY_INSERT dbo.AccountEvents OFF

	DROP TABLE dbo.AccountEvents_Temp
END