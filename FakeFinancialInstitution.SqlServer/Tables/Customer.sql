CREATE TABLE [dbo].[Customer]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [CustomerName] NVARCHAR(128) NOT NULL, 
    [CustomerTypeId] UNIQUEIDENTIFIER NOT NULL, 
    [CustomerPhone] NVARCHAR(128) NOT NULL, 
    [CustomerEmail] NVARCHAR(128) NOT NULL UNIQUE, 
    [CustomerIdNumber] NVARCHAR(128) NOT NULL UNIQUE,
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    [IsActive] BIT NOT NULL,
    CONSTRAINT [FK_Customer_Type_CustomerTypeId] FOREIGN KEY ([CustomerTypeId]) REFERENCES [CustomerType] ([Id]) ON DELETE NO ACTION,
)
