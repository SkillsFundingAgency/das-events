IF EXISTS(SELECT * FROM sys.columns WHERE Name = N'EmployerAccountId' AND Object_ID = Object_ID(N'AccountEvents'))
BEGIN
	SELECT * INTO AccountEvents_Temp FROM dbo.AccountEvents

	DELETE FROM dbo.AccountEvents
END