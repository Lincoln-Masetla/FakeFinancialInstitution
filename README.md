# Introduction
FakeFinancialInstitution
 Backend API for bank employees which can ultimately be consumed by multiple frontends

#Summary
This project ptovides basic functionality to Create a bank account, get acoount details, tranfer funds from one account to another and get tranaction history.

# Project Setup

### Database Project
1. Right click on the FakeFinancialInstitution.SqlServer project 
2. Click publish 
3. Set your target database ie you can use Edit button to navigate to the your prefered database connection target
4. Set database name
5. Copy connection string and add Catalog=DATABASE_NAME on the same connection string to specify the database 
6. Click generate script
7. Once done generating run the script 
8. This will generate your database and some seeding data

### API Project
1. Run the application

# Consuming The API

### Swagger playground

1. Please use the following values for passed ID's

| Name        | Type           | Required  | Description | ValueToUse |
| ------------- |:--------------:|:-----:|------|---------|
| AccountTypeId | GUID | Yes | The Account Type created Savings, Cheque, etc  | 1166BD41-6893-43D4-94D4-38AE057D8B86 |
| CustomerTypeId | GUID | Yes | The Customer type business, personal, etc | 7D110C04-864A-461E-B142-AEAF60FC4341 |
| AccountTransactionTypeId | GUID | Yes | Deposit, Transfer, etc | ba290198-2b44-4acb-b03b-4b1156da30ce |

### Unique Fields
1. CustomerEmail
2. CustomerPhone
3. CustomerIdNumber

### Great! Now Lets create accounts
#### Please use the following Objects to create Accounts ie feel free to make your own values 
#### Please Save the account number so that we can use it for other functionalities 
1. Account 1
```
{
    "Accounts":
    [
        {
            "AccountTypeId":"1166BD41-6893-43D4-94D4-38AE057D8B86",
            "Balance":1000.00,
            "AccountName": "TestAccount"
        }
    ],
    "Customer": 
    {
        "CustomerName": "CustomerName",
        "CustomerTypeId": "7D110C04-864A-461E-B142-AEAF60FC4341",
        "CustomerPhone": "00002385000",
        "CustomerEmail":"te2335@test.com",
        "CustomerIdNumber": "1268567891121"

    }
}
```

2. Account 2
```
{
    "Accounts":
    [
        {
            "AccountTypeId":"1166BD41-6893-43D4-94D4-38AE057D8B86",
            "Balance":1000.00,
            "AccountName": "TestAccount2"
        }
    ],
    "Customer": 
    {
        "CustomerName": "CustomerName2",
        "CustomerTypeId": "7D110C04-864A-461E-B142-AEAF60FC4341",
        "CustomerPhone": "00085635000",
        "CustomerEmail":"test25895@test.com",
        "CustomerIdNumber": "1265268891121"

    }
}
```

### Get Account
1. Use AccountNumber from response of create Account

### Tranfer Funds
1. Please see swagger Api documentation 
2. Enter Values based from the accounts you created.

### Get Transaction History
1. Use Account number to obtain records



