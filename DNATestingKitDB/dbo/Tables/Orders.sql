CREATE TABLE [dbo].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] INT NOT NULL, 
    [Amount] INT NOT NULL, 
    [DeliveryDate] DATETIME NOT NULL
)
