CREATE TABLE [dbo].[CustomerType]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    [CustomerTypeName] NVARCHAR(128) NOT NULL UNIQUE, 
    [CustomerTypeDescription] NVARCHAR(MAX) NULL, 
    [IsActive] BIT NOT NULL,
)
