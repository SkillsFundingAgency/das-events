
-- Migrate Account Events
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


-- Migrate Agreement Events
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

UPDATE dbo.ApprenticeshipEvents
	SET EffectiveFrom = DATEFROMPARTS(DATEPART(year, TrainingStartDate), DATEPART(month, TrainingStartDate), 1)
WHERE EffectiveFrom IS NULL AND [Event] = 'APPRENTICESHIP-AGREEMENT-UPDATED' AND AgreementStatus = 3

BEGIN

	DECLARE @Id BIGINT
	DECLARE @ResourceUrl NVARCHAR(MAX)
	DECLARE @UpdatedUrl NVARCHAR(MAX)

	DECLARE Event_Cursor CURSOR FOR
		SELECT [Id], JSON_VALUE(CAST(Payload AS NVARCHAR(MAX)), '$.ResourceUrl') AS ResourceUrl
		FROM [dbo].[GenericEvents]
		WHERE Type = 'AgreementSignedEvent' AND Payload LIKE '%https%'

	OPEN Event_Cursor;

	FETCH NEXT FROM Event_Cursor INTO @Id, @ResourceUrl
	WHILE @@FETCH_STATUS = 0  
	BEGIN
		SET @UpdatedUrl = SUBSTRING(@ResourceUrl, CHARINDEX('/', @ResourceUrl, 9), 4000)
		UPDATE [dbo].[GenericEvents]
			SET Payload = REPLACE(CAST(Payload AS NVARCHAR(MAX)), @ResourceUrl, @UpdatedUrl)
		WHERE Id = @Id
		FETCH NEXT FROM Event_Cursor INTO @Id, @ResourceUrl
	END;

	CLOSE Event_Cursor
	DEALLOCATE Event_Cursor

END