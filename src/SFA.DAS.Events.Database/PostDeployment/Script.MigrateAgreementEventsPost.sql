IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'AgreementEvents_Temp')
BEGIN
	SET IDENTITY_INSERT dbo.AgreementEvents ON

    INSERT INTO dbo.AgreementEvents
		([Id], [Event], [CreatedOn], [ProviderId], [ContractType])
	SELECT 
		[Id], [Event], [CreatedOn], [ProviderId], 'ProviderAgreement'
	FROM 
		dbo.AgreementEvents_Temp
	WHERE
		[Event] = 'INITIATED'

	SET IDENTITY_INSERT dbo.AgreementEvents OFF

	DROP TABLE dbo.AgreementEvents_Temp
END