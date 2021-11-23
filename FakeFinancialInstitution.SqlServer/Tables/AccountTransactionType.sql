CREATE TABLE [dbo].[AccountTransactionType]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    [TransactionTypeName] NVARCHAR(128) NOT NULL UNIQUE, 
    [TransactionTypeDescription] NVARCHAR(MAX) NULL, 
    [IsActive] BIT NOT NULL,
)
