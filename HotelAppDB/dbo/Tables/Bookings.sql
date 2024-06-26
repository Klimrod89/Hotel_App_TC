﻿CREATE TABLE [dbo].[Bookings]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[RoomId] INT NOT NULL,
	[GuestId] INT NOT NULL,
	[StartDate] DATE NOT NULL,
	[EndDate] DATE NOT NULL,
	[TotalCost] Money NOT NULL,
	[CheckIn] BIT NOT NULL Default 0,
	Constraint [FK_Bookings_Rooms] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Rooms]([Id]),
	Constraint [FK_Bookings_Guests] FOREIGN KEY ([GuestId]) REFERENCES [dbo].[Guests]([Id])
)
