/*
Post-Deployment Script Template                            
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.        
 Use SQLCMD syntax to include a file in the post-deployment script.            
 Example:      :r .\myfile.sql                                
 Use SQLCMD syntax to reference a variable in the post-deployment script.        
 Example:      :setvar TableName MyTable                            
               SELECT * FROM [$(TableName)]                    
--------------------------------------------------------------------------------------
*/
INSERT INTO CustomerType(id,customerTypeName,isActive,customerTypeDescription,createdDate,updatedDate) VALUES ('7d110c04-864a-461e-b142-aeaf60fc4341','Personal',1,'Account owned by an individual',GETDATE(),GETDATE());
INSERT INTO AccountType(id,accountTypeName,isActive,accountTypeDescription,createdDate,updatedDate) VALUES ('1166bd41-6893-43d4-94d4-38ae057d8b86','Savings',1,NULL,GETDATE(),GETDATE());
INSERT INTO AccountTransactionType(id,transactionTypeName,isActive,transactionTypeDescription,createdDate,updatedDate) VALUES ('ba290198-2b44-4acb-b03b-4b1156da30ce','Transfer',1,'Funds deposited from one account to another',GETDATE(),GETDATE());
