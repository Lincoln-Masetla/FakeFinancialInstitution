CREATE TABLE [dbo].[AccountType]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    [AccountTypeName] NVARCHAR(128) NOT NULL UNIQUE, 
    [AccountTypeDescription] NVARCHAR(MAX) NULL, 
    [IsActive] BIT NOT NULL,
)
