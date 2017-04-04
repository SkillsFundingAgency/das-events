CREATE TYPE [dbo].[ApprenticeshipEventsTable] AS TABLE
(
	[Event] VARCHAR(50) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
	[ApprenticeshipId] BIGINT NOT NULL,
	[PaymentOrder] INT NULL, 
	[PaymentStatus] SMALLINT NOT NULL, 
	[AgreementStatus] SMALLINT NOT NULL, 
	[ProviderId] VARCHAR(20) NOT NULL, 
	[LearnerId] VARCHAR(20) NOT NULL, 
	[EmployerAccountId] VARCHAR(20) NOT NULL, 
	[TrainingType] INT NOT NULL, 
	[TrainingId] VARCHAR(20) NOT NULL, 
	[TrainingStartDate] DATETIME NOT NULL, 
	[TrainingEndDate]DATETIME NOT NULL, 
	[TrainingTotalCost] DECIMAL(18,2) NOT NULL,
	[LegalEntityId] NVARCHAR(50) NOT NULL, 
    [LegalEntityName] NVARCHAR(100) NOT NULL, 
    [LegalEntityOrganisationType] NVARCHAR(20) NOT NULL
)
