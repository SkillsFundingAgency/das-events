
-- Migrate Agreement Events
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'AgreementEvents_Temp')
BEGIN
	DROP TABLE dbo.AgreementEvents_Temp
END

IF EXISTS(SELECT * FROM sys.columns WHERE Name = N'EmployerAccountId' AND Object_ID = Object_ID(N'AgreementEvents'))
BEGIN
	SELECT * INTO AgreementEvents_Temp FROM dbo.AgreementEvents

	DELETE FROM dbo.AgreementEvents
END

-- Migrate Account Events
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'AccountEvents_Temp')
BEGIN
	DROP TABLE dbo.AccountEvents_Temp
END

IF EXISTS(SELECT * FROM sys.columns WHERE Name = N'EmployerAccountId' AND Object_ID = Object_ID(N'AccountEvents'))
BEGIN
	SELECT * INTO AccountEvents_Temp FROM dbo.AccountEvents

	DELETE FROM dbo.AccountEvents
END

IF EXISTS(select 1 from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'GenericEvents' and COLUMN_NAME = 'Event')
BEGIN 
	alter table dbo.GenericEvents drop column [Event]
END
GO

IF EXISTS(select 1 from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'GenericEvents' and COLUMN_NAME = 'ResourceUri')
BEGIN 
	alter table dbo.GenericEvents drop column [ResourceUri]
END
GO