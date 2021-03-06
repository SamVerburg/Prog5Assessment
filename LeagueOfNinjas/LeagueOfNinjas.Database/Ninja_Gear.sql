﻿CREATE TABLE [dbo].[Ninja_Gear]
(
	[NinjaID] INT NOT NULL,
	[GearID] INT NOT NULL, 
	PRIMARY KEY(NinjaID,GearID),
    CONSTRAINT [FK_Ninja] FOREIGN KEY (NinjaID) REFERENCES Ninja(ID) on delete cascade, 
    CONSTRAINT [FK_Gear] FOREIGN KEY (GearID) REFERENCES Gear(ID) on delete cascade
)
