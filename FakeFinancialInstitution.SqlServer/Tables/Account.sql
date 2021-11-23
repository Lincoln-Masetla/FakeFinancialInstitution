CREATE TABLE [dbo].[Account]
(
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [AccountNumber] NVARCHAR(128) NOT NULL UNIQUE, 
    [AccountName] NVARCHAR(128) NOT NULL, 
    [AccountTypeId] UNIQUEIDENTIFIER NOT NULL, 
    [CustomerId] UNIQUEIDENTIFIER NOT NULL, 
    [Balance] DECIMAL(18, 2) NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [UpdatedDate] DATETIME NOT NULL, 
    CONSTRAINT [FK_Account_Type_AccountTypeId] FOREIGN KEY ([AccountTypeId]) REFERENCES [AccountType] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Account_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id]) ON DELETE NO ACTION,
  
)


