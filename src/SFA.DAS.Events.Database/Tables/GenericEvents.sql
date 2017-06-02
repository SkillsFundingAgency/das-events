CREATE TABLE [dbo].[GenericEvents]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
	[Type] VARCHAR(255) NOT NULL,
	[Payload] TEXT NOT NULL,
    [CreatedOn] DATETIME NOT NULL, 
)
GO
CREATE NONCLUSTERED INDEX [IX_GenericEvents_Type_Id] ON [dbo].[GenericEvents] ([Type], [Id])
GO
CREATE NONCLUSTERED INDEX [IX_GenericEvents_Type_CreatedOn] ON [dbo].[GenericEvents] ([Type], [CreatedOn])