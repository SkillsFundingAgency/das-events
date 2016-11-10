CREATE TABLE [dbo].[AgreementEvents]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
    [Event] VARCHAR(50) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [ProviderId] VARCHAR(20) NOT NULL, 
    [EmployerAccountId] VARCHAR(20) NOT NULL
)

GO

CREATE INDEX [IX_AgreementEvents_CreatedOn] ON [dbo].[AgreementEvents] ([CreatedOn])
