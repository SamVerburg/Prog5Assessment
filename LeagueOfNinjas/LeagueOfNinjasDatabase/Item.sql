CREATE TABLE [dbo].[Item] (
    [Id] INT IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [Name]  VARCHAR (50) NOT NULL,
    [Price] INT NOT NULL,
    [Category] VARCHAR (50) NOT NULL,
    [NinjaId] INT NULL,
    CONSTRAINT [FK_Ninja_Item] FOREIGN KEY ([NinjaId]) REFERENCES [dbo].[Ninja] ([Id])
);


