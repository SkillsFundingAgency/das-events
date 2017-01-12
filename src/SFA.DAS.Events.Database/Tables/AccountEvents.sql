CREATE TABLE [dbo].[AccountEvents]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
    [Event] VARCHAR(50) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [EmployerAccountId] VARCHAR(20) NOT NULL
)

GO

CREATE INDEX [IX_AccountEvents_CreatedOn] ON [dbo].[AccountEvents] ([CreatedOn])
