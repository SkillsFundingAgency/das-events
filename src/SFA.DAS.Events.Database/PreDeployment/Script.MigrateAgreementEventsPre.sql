IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'AgreementEvents_Temp')
BEGIN
	DROP TABLE dbo.AgreementEvents_Temp
END

IF EXISTS(SELECT * FROM sys.columns WHERE Name = N'EmployerAccountId' AND Object_ID = Object_ID(N'AgreementEvents'))
BEGIN
	SELECT * INTO AgreementEvents_Temp FROM dbo.AgreementEvents

	DELETE FROM dbo.AgreementEvents
END