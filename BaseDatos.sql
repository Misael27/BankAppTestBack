USE master;
GO

IF DB_ID('BankAppTestDB') IS NOT NULL
BEGIN
    ALTER DATABASE BankAppTestDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE BankAppTestDB;
END
GO

CREATE DATABASE BankAppTestDB;
GO

USE BankAppTestDB;
GO

-- Tabla: Clients
IF OBJECT_ID('dbo.Clients', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Clients (
        Id BIGINT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Gender INT NOT NULL,
        Birthdate DATE NOT NULL,
        Person_Id NVARCHAR(50) UNIQUE NOT NULL, 
        Address NVARCHAR(255) NOT NULL,
        Phone NVARCHAR(25) NOT NULL,
        Password NVARCHAR(255) NOT NULL,
        State BIT DEFAULT 1
    );
END
GO

-- Tabla: Accounts
IF OBJECT_ID('dbo.Accounts', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Accounts (
        Id BIGINT IDENTITY(1,1) PRIMARY KEY,
        Number NVARCHAR(50) UNIQUE NOT NULL,
        Type NVARCHAR(50) NOT NULL,
        Init_Balance FLOAT,
        State BIT DEFAULT 1,
        ClientId BIGINT NOT NULL 
    );
END
GO

-- Tabla: Movements
IF OBJECT_ID('dbo.Movements', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Movements (
        Id BIGINT IDENTITY(1,1) PRIMARY KEY,
        Date DATETIME DEFAULT GETDATE(),
        Type NVARCHAR(50) NOT NULL,
        Value FLOAT,
        Balance FLOAT,
        AccountId BIGINT NOT NULL
    );
END
GO

-- FK para Accounts.ClientId
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Accounts_Client' AND parent_object_id = OBJECT_ID('dbo.Accounts'))
BEGIN
    ALTER TABLE dbo.Accounts 
    ADD CONSTRAINT FK_Accounts_Client 
    FOREIGN KEY (ClientId) REFERENCES dbo.Clients (Id);
END
GO

-- FK para Movements.AccountId
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Movements_Account' AND parent_object_id = OBJECT_ID('dbo.Movements'))
BEGIN
    ALTER TABLE dbo.Movements 
    ADD CONSTRAINT FK_Movements_Account 
    FOREIGN KEY (AccountId) REFERENCES dbo.Accounts (Id);
END
GO