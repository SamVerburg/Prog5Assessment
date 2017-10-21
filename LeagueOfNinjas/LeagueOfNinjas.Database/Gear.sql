CREATE TABLE [dbo].[Gear]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Price] INT NOT NULL, 
    [Category] VARCHAR(50) NOT NULL, 
    [Intelligence] INT NULL, 
    [Strength] INT NULL, 
    [Agility] INT NULL
)
