CREATE TABLE [dbo].[ApprenticeshipEvents]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
    [Event] VARCHAR(50) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
	[ApprenticeshipId] BIGINT NOT NULL,
	[PaymentOrder] INT NULL, 
	[PaymentStatus] SMALLINT NOT NULL, 
	[AgreementStatus] SMALLINT NOT NULL, 
	[ProviderId] VARCHAR(20) NULL, 
	[LearnerId] VARCHAR(20) NULL, 
	[EmployerAccountId] VARCHAR(20) NOT NULL, 
	[TrainingType] INT NOT NULL, 
	[TrainingId] VARCHAR(20) NULL, 
	[TrainingStartDate] DATETIME NULL, 
	[TrainingEndDate]DATETIME NULL, 
	[TrainingTotalCost] DECIMAL(18,2) NULL, 
    [LegalEntityId] NVARCHAR(50) NOT NULL DEFAULT '', 
    [LegalEntityName] NVARCHAR(100) NOT NULL DEFAULT '', 
    [LegalEntityOrganisationType] NVARCHAR(20) NOT NULL DEFAULT '', 
    [EffectiveFrom] DATETIME NULL, 
    [EffectiveTo] DATETIME NULL, 
    [DateOfBirth] DATETIME NULL
)

GO

CREATE INDEX [IX_ApprenticeshipEvents_CreatedOn] ON [dbo].[ApprenticeshipEvents] ([CreatedOn])
