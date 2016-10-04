CREATE TABLE [dbo].[ApprenticeshipEvents]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
    [CreatedOn] DATETIME NOT NULL, 
    [Event] VARCHAR(50) NOT NULL, 
    [EventType] VARCHAR(50) NOT NULL, 
	[Data] VARCHAR(MAX) NULL
)
