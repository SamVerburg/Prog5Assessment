CREATE TABLE [dbo].[Ninja]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Gold] INT NOT NULL, 
    [Strength] INT NOT NULL DEFAULT 0 , 
    [Intelligence] INT NOT NULL DEFAULT 0 , 
    [Agility] INT NOT NULL DEFAULT 0  
)
