
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/02/2018 12:00:56
-- Generated from EDMX file: E:\Notes\PleaseStayWithUs\PleaseStayWithUs\PleaseStayWithUs\Data Access Layer\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TrainingManagement];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Course__TrainerI__2EA5EC27]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Course] DROP CONSTRAINT [FK__Course__TrainerI__2EA5EC27];
GO
IF OBJECT_ID(N'[dbo].[FK__Employee__MID__1DB06A4F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [FK__Employee__MID__1DB06A4F];
GO
IF OBJECT_ID(N'[dbo].[FK__Employee__UserNa__1CBC4616]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [FK__Employee__UserNa__1CBC4616];
GO
IF OBJECT_ID(N'[dbo].[FK__Feedback__Course__3B0BC30C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Feedback] DROP CONSTRAINT [FK__Feedback__Course__3B0BC30C];
GO
IF OBJECT_ID(N'[dbo].[FK__Feedback__Employ__3BFFE745]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Feedback] DROP CONSTRAINT [FK__Feedback__Employ__3BFFE745];
GO
IF OBJECT_ID(N'[dbo].[FK__Feedback__Manage__3CF40B7E]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Feedback] DROP CONSTRAINT [FK__Feedback__Manage__3CF40B7E];
GO
IF OBJECT_ID(N'[dbo].[FK__Manager__UserNam__19DFD96B]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Manager] DROP CONSTRAINT [FK__Manager__UserNam__19DFD96B];
GO
IF OBJECT_ID(N'[dbo].[FK__Request__CourseI__382F5661]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Request] DROP CONSTRAINT [FK__Request__CourseI__382F5661];
GO
IF OBJECT_ID(N'[dbo].[FK__Request__Employe__36470DEF]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Request] DROP CONSTRAINT [FK__Request__Employe__36470DEF];
GO
IF OBJECT_ID(N'[dbo].[FK__Request__Manager__373B3228]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Request] DROP CONSTRAINT [FK__Request__Manager__373B3228];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admin];
GO
IF OBJECT_ID(N'[dbo].[Course]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Course];
GO
IF OBJECT_ID(N'[dbo].[Domain]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Domain];
GO
IF OBJECT_ID(N'[dbo].[Employee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee];
GO
IF OBJECT_ID(N'[dbo].[Feedback]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Feedback];
GO
IF OBJECT_ID(N'[dbo].[Login]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Login];
GO
IF OBJECT_ID(N'[dbo].[Manager]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Manager];
GO
IF OBJECT_ID(N'[dbo].[Request]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Request];
GO
IF OBJECT_ID(N'[dbo].[SecurityQuestion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SecurityQuestion];
GO
IF OBJECT_ID(N'[dbo].[Trainer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trainer];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Admins'
CREATE TABLE [dbo].[Admins] (
    [AdminID] varchar(20)  NOT NULL,
    [AdminName] varchar(20)  NULL,
    [AdminMail] varchar(20)  NULL,
    [AdminPassword] varchar(20)  NULL
);
GO

-- Creating table 'Courses'
CREATE TABLE [dbo].[Courses] (
    [CourseID] int IDENTITY(1,1) NOT NULL,
    [CourseName] varchar(20)  NULL,
    [DomainName] varchar(20)  NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [Session] varchar(20)  NULL,
    [TrainerID] int  NULL
);
GO

-- Creating table 'Domains'
CREATE TABLE [dbo].[Domains] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [DomainName] varchar(20)  NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [EID] int IDENTITY(1,1) NOT NULL,
    [UserID] varchar(23)  NULL,
    [Name] varchar(20)  NULL,
    [Age] int  NULL,
    [Gender] varchar(20)  NULL,
    [EmailID] varchar(20)  NULL,
    [Pwd] varchar(20)  NULL,
    [SecurityQuestion] varchar(50)  NULL,
    [Answer] varchar(50)  NULL,
    [Designation] varchar(20)  NULL,
    [UserName] varchar(20)  NULL,
    [MID] int  NULL
);
GO

-- Creating table 'Feedbacks'
CREATE TABLE [dbo].[Feedbacks] (
    [fid] int  NOT NULL,
    [CourseID] int  NOT NULL,
    [EmployeeID] int  NULL,
    [ManagerID] int  NULL,
    [TrainerID] int  NULL,
    [Score] varchar(20)  NULL
);
GO

-- Creating table 'Logins'
CREATE TABLE [dbo].[Logins] (
    [UserName] varchar(20)  NOT NULL,
    [Pwd] varchar(20)  NULL,
    [Designation] varchar(20)  NULL
);
GO

-- Creating table 'Managers'
CREATE TABLE [dbo].[Managers] (
    [MID] int IDENTITY(1,1) NOT NULL,
    [ManagerUserID] varchar(23)  NULL,
    [Name] varchar(20)  NULL,
    [Age] int  NULL,
    [Gender] varchar(20)  NULL,
    [EmailID] varchar(20)  NULL,
    [Pwd] varchar(20)  NULL,
    [SecurityQuestion] varchar(50)  NULL,
    [Designation] varchar(20)  NULL,
    [Answer] varchar(50)  NULL,
    [UserName] varchar(20)  NULL
);
GO

-- Creating table 'Requests'
CREATE TABLE [dbo].[Requests] (
    [RequestID] int IDENTITY(1,1) NOT NULL,
    [EmployeeID] int  NULL,
    [ManagerID] int  NULL,
    [CourseID] int  NULL,
    [StartDate] datetime  NULL,
    [EndDate] datetime  NULL,
    [Session] varchar(20)  NULL,
    [CourseName] varchar(20)  NULL,
    [Status] varchar(20)  NULL,
    [Reason] varchar(20)  NULL
);
GO

-- Creating table 'SecurityQuestions'
CREATE TABLE [dbo].[SecurityQuestions] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Question] varchar(50)  NULL
);
GO

-- Creating table 'Trainers'
CREATE TABLE [dbo].[Trainers] (
    [TrainerID] int IDENTITY(1,1) NOT NULL,
    [TrainerName] varchar(20)  NULL,
    [TrainerMail] varchar(30)  NULL,
    [Qualification] varchar(30)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AdminID] in table 'Admins'
ALTER TABLE [dbo].[Admins]
ADD CONSTRAINT [PK_Admins]
    PRIMARY KEY CLUSTERED ([AdminID] ASC);
GO

-- Creating primary key on [CourseID] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [PK_Courses]
    PRIMARY KEY CLUSTERED ([CourseID] ASC);
GO

-- Creating primary key on [ID] in table 'Domains'
ALTER TABLE [dbo].[Domains]
ADD CONSTRAINT [PK_Domains]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [EID] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([EID] ASC);
GO

-- Creating primary key on [fid] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [PK_Feedbacks]
    PRIMARY KEY CLUSTERED ([fid] ASC);
GO

-- Creating primary key on [UserName] in table 'Logins'
ALTER TABLE [dbo].[Logins]
ADD CONSTRAINT [PK_Logins]
    PRIMARY KEY CLUSTERED ([UserName] ASC);
GO

-- Creating primary key on [MID] in table 'Managers'
ALTER TABLE [dbo].[Managers]
ADD CONSTRAINT [PK_Managers]
    PRIMARY KEY CLUSTERED ([MID] ASC);
GO

-- Creating primary key on [RequestID] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [PK_Requests]
    PRIMARY KEY CLUSTERED ([RequestID] ASC);
GO

-- Creating primary key on [ID] in table 'SecurityQuestions'
ALTER TABLE [dbo].[SecurityQuestions]
ADD CONSTRAINT [PK_SecurityQuestions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [TrainerID] in table 'Trainers'
ALTER TABLE [dbo].[Trainers]
ADD CONSTRAINT [PK_Trainers]
    PRIMARY KEY CLUSTERED ([TrainerID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TrainerID] in table 'Courses'
ALTER TABLE [dbo].[Courses]
ADD CONSTRAINT [FK__Course__TrainerI__2EA5EC27]
    FOREIGN KEY ([TrainerID])
    REFERENCES [dbo].[Trainers]
        ([TrainerID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Course__TrainerI__2EA5EC27'
CREATE INDEX [IX_FK__Course__TrainerI__2EA5EC27]
ON [dbo].[Courses]
    ([TrainerID]);
GO

-- Creating foreign key on [CourseID] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [FK__Feedback__Course__3B0BC30C]
    FOREIGN KEY ([CourseID])
    REFERENCES [dbo].[Courses]
        ([CourseID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Feedback__Course__3B0BC30C'
CREATE INDEX [IX_FK__Feedback__Course__3B0BC30C]
ON [dbo].[Feedbacks]
    ([CourseID]);
GO

-- Creating foreign key on [CourseID] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [FK__Request__CourseI__382F5661]
    FOREIGN KEY ([CourseID])
    REFERENCES [dbo].[Courses]
        ([CourseID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Request__CourseI__382F5661'
CREATE INDEX [IX_FK__Request__CourseI__382F5661]
ON [dbo].[Requests]
    ([CourseID]);
GO

-- Creating foreign key on [MID] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK__Employee__MID__1DB06A4F]
    FOREIGN KEY ([MID])
    REFERENCES [dbo].[Managers]
        ([MID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Employee__MID__1DB06A4F'
CREATE INDEX [IX_FK__Employee__MID__1DB06A4F]
ON [dbo].[Employees]
    ([MID]);
GO

-- Creating foreign key on [UserName] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK__Employee__UserNa__1CBC4616]
    FOREIGN KEY ([UserName])
    REFERENCES [dbo].[Logins]
        ([UserName])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Employee__UserNa__1CBC4616'
CREATE INDEX [IX_FK__Employee__UserNa__1CBC4616]
ON [dbo].[Employees]
    ([UserName]);
GO

-- Creating foreign key on [EmployeeID] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [FK__Feedback__Employ__3BFFE745]
    FOREIGN KEY ([EmployeeID])
    REFERENCES [dbo].[Employees]
        ([EID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Feedback__Employ__3BFFE745'
CREATE INDEX [IX_FK__Feedback__Employ__3BFFE745]
ON [dbo].[Feedbacks]
    ([EmployeeID]);
GO

-- Creating foreign key on [EmployeeID] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [FK__Request__Employe__36470DEF]
    FOREIGN KEY ([EmployeeID])
    REFERENCES [dbo].[Employees]
        ([EID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Request__Employe__36470DEF'
CREATE INDEX [IX_FK__Request__Employe__36470DEF]
ON [dbo].[Requests]
    ([EmployeeID]);
GO

-- Creating foreign key on [ManagerID] in table 'Feedbacks'
ALTER TABLE [dbo].[Feedbacks]
ADD CONSTRAINT [FK__Feedback__Manage__3CF40B7E]
    FOREIGN KEY ([ManagerID])
    REFERENCES [dbo].[Managers]
        ([MID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Feedback__Manage__3CF40B7E'
CREATE INDEX [IX_FK__Feedback__Manage__3CF40B7E]
ON [dbo].[Feedbacks]
    ([ManagerID]);
GO

-- Creating foreign key on [UserName] in table 'Managers'
ALTER TABLE [dbo].[Managers]
ADD CONSTRAINT [FK__Manager__UserNam__19DFD96B]
    FOREIGN KEY ([UserName])
    REFERENCES [dbo].[Logins]
        ([UserName])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Manager__UserNam__19DFD96B'
CREATE INDEX [IX_FK__Manager__UserNam__19DFD96B]
ON [dbo].[Managers]
    ([UserName]);
GO

-- Creating foreign key on [ManagerID] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [FK__Request__Manager__373B3228]
    FOREIGN KEY ([ManagerID])
    REFERENCES [dbo].[Managers]
        ([MID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Request__Manager__373B3228'
CREATE INDEX [IX_FK__Request__Manager__373B3228]
ON [dbo].[Requests]
    ([ManagerID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------