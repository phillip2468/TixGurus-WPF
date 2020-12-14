
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/21/2019 17:12:32
-- Generated from EDMX file: C:\Users\PC USER\source\repos\TixGurus\Ssample\Model\Customer_Details_Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CustomerDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customer_Details]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customer_Details];
GO
IF OBJECT_ID(N'[dbo].[Employee_Details]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee_Details];
GO
IF OBJECT_ID(N'[dbo].[Event_Details]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Event_Details];
GO
IF OBJECT_ID(N'[dbo].[Location_Details]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Location_Details];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customer_Details'
CREATE TABLE [dbo].[Customer_Details] (
    [Customer_ID] int IDENTITY(1,1) NOT NULL,
    [Email] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [First_Name] varchar(50)  NOT NULL,
    [Last_Name] varchar(50)  NOT NULL,
    [Phone_Number] int  NOT NULL,
    [Address] varchar(50)  NOT NULL,
    [City] varchar(50)  NOT NULL,
    [State] char(10)  NOT NULL,
    [Postcode] int  NOT NULL,
    [Date_Of_Birth] datetime  NOT NULL,
    [Date_Created] datetime  NOT NULL
);
GO

-- Creating table 'Employee_Details'
CREATE TABLE [dbo].[Employee_Details] (
    [Employee_ID] int  NOT NULL,
    [First_Name] nvarchar(50)  NOT NULL,
    [Last_Name] nvarchar(50)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Location_Details'
CREATE TABLE [dbo].[Location_Details] (
    [Place_ID] nvarchar(50)  NOT NULL,
    [Place_Name] nvarchar(50)  NOT NULL,
    [Capacity] nvarchar(50)  NOT NULL,
    [Location] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Event_Details'
CREATE TABLE [dbo].[Event_Details] (
    [Event_ID] int  NOT NULL,
    [Event_Title] nvarchar(50)  NOT NULL,
    [Event_Description] nvarchar(max)  NOT NULL,
    [Event_Date_And_Time] datetime  NOT NULL,
    [Event_Location] nvarchar(50)  NOT NULL,
    [Event_Picture] varbinary(max)  NOT NULL,
    [Event_Pricing_1] decimal(19,4)  NOT NULL,
    [Event_Pricing_2] decimal(19,4)  NULL,
    [Event_Pricing_3] decimal(19,4)  NULL,
    [Event_Pricing_4] decimal(19,4)  NULL,
    [Event_Pricing_5] decimal(19,4)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Customer_ID] in table 'Customer_Details'
ALTER TABLE [dbo].[Customer_Details]
ADD CONSTRAINT [PK_Customer_Details]
    PRIMARY KEY CLUSTERED ([Customer_ID] ASC);
GO

-- Creating primary key on [Employee_ID] in table 'Employee_Details'
ALTER TABLE [dbo].[Employee_Details]
ADD CONSTRAINT [PK_Employee_Details]
    PRIMARY KEY CLUSTERED ([Employee_ID] ASC);
GO

-- Creating primary key on [Place_ID] in table 'Location_Details'
ALTER TABLE [dbo].[Location_Details]
ADD CONSTRAINT [PK_Location_Details]
    PRIMARY KEY CLUSTERED ([Place_ID] ASC);
GO

-- Creating primary key on [Event_ID] in table 'Event_Details'
ALTER TABLE [dbo].[Event_Details]
ADD CONSTRAINT [PK_Event_Details]
    PRIMARY KEY CLUSTERED ([Event_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------