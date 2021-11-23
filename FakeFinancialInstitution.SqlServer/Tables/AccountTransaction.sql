CREATE TABLE [dbo].[AccountTransaction]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NULL, 
    [TransactionTypeId] UNIQUEIDENTIFIER NOT NULL, 
    [TransactionAmount] DECIMAL(18, 2) NOT NULL, 
    [FromAccountId] UNIQUEIDENTIFIER NOT NULL, 
    [ToAccountId] UNIQUEIDENTIFIER NOT NULL, 
    [FromReference] NVARCHAR(128) NOT NULL,
    [ToReference] NVARCHAR(128) NOT NULL,
    [FromAccountBalance] DECIMAL(18, 2) NOT NULL, 
    [ToAccountBalance] DECIMAL(18, 2) NOT NULL, 
    [FromAccountBeforeBalance] DECIMAL(18, 2) NOT NULL, 
    [ToAccountBeforeBalance] DECIMAL(18, 2) NOT NULL, 
    CONSTRAINT [FK_AccountTransaction_Type_TransactionTypeId] FOREIGN KEY ([TransactionTypeId]) REFERENCES [AccountTransactionType] ([Id]) ON DELETE NO ACTION,

)
