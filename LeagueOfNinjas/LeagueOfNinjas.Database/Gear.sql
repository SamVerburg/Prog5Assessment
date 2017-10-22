CREATE TABLE [dbo].[Gear]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Price] INT NOT NULL, 
    [Category] VARCHAR(50) NOT NULL, 
    [Intelligence] INT NOT NULL DEFAULT 0  , 
    [Strength] INT NOT NULL DEFAULT 0  , 
    [Agility] INT NOT NULL DEFAULT 0  
)
