CREATE TABLE [dbo].[PriceHistory]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
	[ApprenticeshipEventsId] BIGINT NOT NULL,
	[TotalCost] DECIMAL NOT NULL,
	[EffectiveFrom] DateTime NOT NULL,
	[EffectiveTo] DateTime NULL,
	CONSTRAINT [FK_PriceHistory_ApprenticeshipEvents] FOREIGN KEY ([ApprenticeshipEventsId]) REFERENCES [ApprenticeshipEvents]([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_PriceHistory_ApprenticeshipEventsId] ON [dbo].[PriceHistory] ([ApprenticeshipEventsId]) INCLUDE ([TotalCost], [EffectiveFrom], [EffectiveTo]) WITH (ONLINE = ON)