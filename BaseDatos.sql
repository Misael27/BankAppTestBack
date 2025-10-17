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
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Gender NVARCHAR(10) NOT NULL,
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
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Number NVARCHAR(50) UNIQUE NOT NULL,
        Type TINYINT NOT NULL,
        Init_Balance DECIMAL(14, 2),
        State BIT DEFAULT 1,
        Client_Id INT NOT NULL
    );
END
GO

-- Tabla: Movements
IF OBJECT_ID('dbo.Movements', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Movements (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Date DATETIME DEFAULT GETDATE(),
        Type TINYINT NOT NULL,
        Value DECIMAL(14, 2),
        Balance DECIMAL(14, 2),
        Account_Id INT NOT NULL
    );
END
GO

-- FK para Accounts.Client_Id
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Accounts_Client' AND parent_object_id = OBJECT_ID('dbo.Accounts'))
BEGIN
    ALTER TABLE dbo.Accounts 
    ADD CONSTRAINT FK_Accounts_Client 
    FOREIGN KEY (Client_Id) REFERENCES dbo.Clients (Id);
END
GO

-- FK para Movements.Account_Id
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Movements_Account' AND parent_object_id = OBJECT_ID('dbo.Movements'))
BEGIN
    ALTER TABLE dbo.Movements 
    ADD CONSTRAINT FK_Movements_Account 
    FOREIGN KEY (Account_Id) REFERENCES dbo.Accounts (Id);
END
GO