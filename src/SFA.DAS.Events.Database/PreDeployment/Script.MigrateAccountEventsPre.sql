IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'AccountEvents_Temp')
BEGIN
	DROP TABLE dbo.AccountEvents_Temp
END

IF EXISTS(SELECT * FROM sys.columns WHERE Name = N'EmployerAccountId' AND Object_ID = Object_ID(N'AccountEvents'))
BEGIN
	SELECT * INTO AccountEvents_Temp FROM dbo.AccountEvents

	DELETE FROM dbo.AccountEvents
END