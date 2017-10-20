﻿CREATE TABLE [dbo].[Ninja_Gear]
(
	[NinjaID] INT NOT NULL PRIMARY KEY,
	[GearID] INT NOT NULL PRIMARY KEY, 
	PRIMARY KEY(NinjaID,GearID),
    CONSTRAINT [FK_Ninja] FOREIGN KEY (NinjaID) REFERENCES Ninja(ID), 
    CONSTRAINT [FK_Gear] FOREIGN KEY (GearID) REFERENCES Gear(ID)
)
