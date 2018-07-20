﻿CREATE TYPE [dbo].[ApprenticeshipEventsType] AS TABLE(
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
    [DateOfBirth] DATETIME NULL,
	[TransferSenderId] BIGINT NULL,
	[TransferSenderName] NVARCHAR(100) NULL,
	[TransferApprovalStatus] TINYINT NULL,
	[TransferApprovalActionedOn] DATETIME2 NULL,
	-- todo: the ApprenticeshipEvents table needs this field nullable so that the update deploy will work
	-- but we could have it as not nullable in the type, which should force any new additions to have it (it is logically mandatory)
	[AccountLegalEntityPublicHashedId] CHAR(6) NULL
)

