CREATE TABLE [dbo].[ApprenticeshipEvents]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
    [Event] VARCHAR(50) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
	[PaymentStatus] VARCHAR(50) NOT NULL, 
	[AgreementStatus] VARCHAR(50) NOT NULL, 
	[ProviderId] VARCHAR(20) NOT NULL, 
	[LearnerId] VARCHAR(20) NOT NULL, 
	[EmployerAccountId] VARCHAR(20) NOT NULL, 
	[TrainingType] INT NOT NULL, 
	[TrainingId] VARCHAR(20) NOT NULL, 
	[TrainingStartDate] DATETIME NOT NULL, 
	[TrainingEndDate]DATETIME NOT NULL, 
	[TrainingTotalCost] DECIMAL(18,2) NOT NULL
)
