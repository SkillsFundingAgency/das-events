CREATE TABLE [dbo].[GenericEvents]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Event] VARCHAR(50) NOT NULL, 
	[Type] VARCHAR(255) NOT NULL,
	[Payload] TEXT NOT NULL,
    [CreatedOn] DATETIME NOT NULL, 
)
