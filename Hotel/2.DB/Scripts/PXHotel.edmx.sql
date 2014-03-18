
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/18/2014 15:29:25
-- Generated from EDMX file: D:\Web Projects\PX Hotel\Source\trunk\PX.EntityModel\PXHotel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PXHotel];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Menu_Menu]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Menus] DROP CONSTRAINT [FK_Menu_Menu];
GO
IF OBJECT_ID(N'[dbo].[FK_NewsNewsCategory_News]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NewsNewsCategories] DROP CONSTRAINT [FK_NewsNewsCategory_News];
GO
IF OBJECT_ID(N'[dbo].[FK_NewsNewsCategory_NewsCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NewsNewsCategories] DROP CONSTRAINT [FK_NewsNewsCategory_NewsCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_Resources_Countries]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Resources] DROP CONSTRAINT [FK_Resources_Countries];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleMenu_Menu]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupMenus] DROP CONSTRAINT [FK_RoleMenu_Menu];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleMenu_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupMenus] DROP CONSTRAINT [FK_RoleMenu_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_Room_Room]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rooms] DROP CONSTRAINT [FK_Room_Room];
GO
IF OBJECT_ID(N'[dbo].[FK_User_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_User_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_User_Status]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_User_Status];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[BookingRequests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BookingRequests];
GO
IF OBJECT_ID(N'[dbo].[Countries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countries];
GO
IF OBJECT_ID(N'[dbo].[GroupMenus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupMenus];
GO
IF OBJECT_ID(N'[dbo].[Menus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Menus];
GO
IF OBJECT_ID(N'[dbo].[News]', 'U') IS NOT NULL
    DROP TABLE [dbo].[News];
GO
IF OBJECT_ID(N'[dbo].[NewsCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NewsCategories];
GO
IF OBJECT_ID(N'[dbo].[NewsNewsCategories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NewsNewsCategories];
GO
IF OBJECT_ID(N'[dbo].[Pages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pages];
GO
IF OBJECT_ID(N'[dbo].[Resources]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Resources];
GO
IF OBJECT_ID(N'[dbo].[Rooms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rooms];
GO
IF OBJECT_ID(N'[dbo].[RoomTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoomTypes];
GO
IF OBJECT_ID(N'[dbo].[SiteSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SiteSettings];
GO
IF OBJECT_ID(N'[dbo].[Status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Status];
GO
IF OBJECT_ID(N'[dbo].[UserGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserGroups];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BookingRequests'
CREATE TABLE [dbo].[BookingRequests] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FullName] nvarchar(512)  NOT NULL,
    [Email] nvarchar(512)  NOT NULL,
    [IndentityNumber] nvarchar(128)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Country] nvarchar(512)  NOT NULL,
    [Phone] nvarchar(512)  NOT NULL,
    [Note] nvarchar(max)  NULL,
    [TotalRooms] int  NOT NULL,
    [BookingFrom] datetime  NOT NULL,
    [BookingTo] datetime  NOT NULL,
    [RecordOrder] int  NOT NULL,
    [RecordActive] bit  NOT NULL,
    [Created] datetime  NOT NULL,
    [CreatedBy] nvarchar(512)  NOT NULL,
    [Updated] datetime  NULL,
    [UpdatedBy] nvarchar(512)  NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(512)  NOT NULL,
    [RecordOrder] int  NOT NULL,
    [RecordActive] bit  NOT NULL,
    [Created] datetime  NOT NULL,
    [CreatedBy] nvarchar(512)  NOT NULL,
    [Updated] datetime  NULL,
    [UpdatedBy] nvarchar(512)  NULL
);
GO

-- Creating table 'GroupMenus'
CREATE TABLE [dbo].[GroupMenus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserGroupId] int  NOT NULL,
    [MenuId] int  NOT NULL,
    [RecordOrder] int  NOT NULL,
    [RecordActive] bit  NOT NULL,
    [Created] datetime  NOT NULL,
    [CreatedBy] nvarchar(512)  NOT NULL,
    [Updated] datetime  NULL,
    [UpdatedBy] nvarchar(512)  NULL
);
GO

-- Creating table 'Menus'
CREATE TABLE [dbo].[Menus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(256)  NOT NULL,
    [Url] nvarchar(512)  NULL,
    [Controller] nvarchar(512)  NULL,
    [Action] nvarchar(512)  NULL,
    [ParentId] int  NULL,
    [Hierarchy] nvarchar(max)  NOT NULL,
    [MenuIcon] nvarchar(512)  NULL,
    [RecordActive] bit  NOT NULL,
    [RecordOrder] int  NOT NULL,
    [Created] datetime  NOT NULL,
    [CreatedBy] nvarchar(512)  NOT NULL,
    [Updated] datetime  NULL,
    [UpdatedBy] nvarchar(512)  NULL
);
GO

-- Creating table 'News'
CREATE TABLE [dbo].[News] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [ImageFileName] nvarchar(512)  NULL,
    [Status] int  NOT NULL,
    [RecordOrder] int  NOT NULL,
    [RecordActive] bit  NOT NULL,
    [Created] datetime  NOT NULL,
    [CreatedBy] nvarchar(512)  NOT NULL,
    [Updated] datetime  NULL,
    [UpdatedBy] nvarchar(512)  NULL
);
GO

-- Creating table 'NewsCategories'
CREATE TABLE [dbo].[NewsCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(512)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [RecordOrder] int  NOT NULL,
    [RecordActive] bit  NOT NULL,
    [Created] datetime  NOT NULL,
    [CreatedBy] nvarchar(512)  NOT NULL,
    [Updated] datetime  NULL,
    [UpdatedBy] nvarchar(512)  NULL
);
GO

-- Creating table 'NewsNewsCategories'
CREATE TABLE [dbo].[NewsNewsCategories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NewsId] int  NOT NULL,
    [NewsCategoryId] int  NOT NULL
);
GO

-- Creating table 'Pages'
CREATE TABLE [dbo].[Pages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(512)  NOT NULL,
    [Caption] nvarchar(max)  NULL,
    [Caption_Working] nvarchar(max)  NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Content_Working] nvarchar(max)  NOT NULL,
    [StatusId] int  NOT NULL,
    [FriendlyUrl] nvarchar(512)  NOT NULL,
    [RecordOrder] int  NOT NULL,
    [RecordActive] bit  NOT NULL,
    [Created] datetime  NOT NULL,
    [CreatedBy] nvarchar(512)  NOT NULL,
    [Updated] datetime  NULL,
    [UpdatedBy] nvarchar(512)  NULL
);
GO

-- Creating table 'Resources'
CREATE TABLE [dbo].[Resources] (
    [Id] int  NOT NULL,
    [Name] nvarchar(512)  NOT NULL,
    [DefaultValue] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [CountryId] int  NOT NULL,
    [RecordOrder] int  NOT NULL,
    [RecordActive] bit  NOT NULL,
    [Created] datetime  NOT NULL,
    [CreatedBy] nvarchar(512)  NOT NULL,
    [Updated] datetime  NULL,
    [UpdatedBy] nvarchar(512)  NULL
);
GO

-- Creating table 'Rooms'
CREATE TABLE [dbo].[Rooms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(512)  NOT NULL,
    [Note] nvarchar(max)  NULL,
    [StatusId] int  NOT NULL,
    [RoomTypeId] int  NOT NULL,
    [RecordOrder] int  NOT NULL,
    [RecordActive] bit  NOT NULL,
    [Created] datetime  NOT NULL,
    [CreatedBy] nvarchar(512)  NOT NULL,
    [Updated] datetime  NULL,
    [UpdatedBy] nvarchar(512)  NULL
);
GO

-- Creating table 'RoomTypes'
CREATE TABLE [dbo].[RoomTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(512)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Price] float  NOT NULL,
    [RecordOrder] int  NOT NULL,
    [RecordActive] bit  NOT NULL,
    [Created] datetime  NOT NULL,
    [CreatedDate] nvarchar(512)  NOT NULL,
    [Updated] datetime  NULL,
    [UpdatedBy] nvarchar(512)  NULL
);
GO

-- Creating table 'SiteSettings'
CREATE TABLE [dbo].[SiteSettings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(512)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [RecordOrder] int  NOT NULL,
    [RecordActive] bit  NOT NULL,
    [Created] datetime  NOT NULL,
    [CreatedBy] nvarchar(512)  NOT NULL,
    [Updated] datetime  NULL,
    [UpdatedBy] nvarchar(512)  NULL
);
GO

-- Creating table 'Status'
CREATE TABLE [dbo].[Status] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(256)  NOT NULL,
    [Description] nvarchar(512)  NULL,
    [RecordOrder] int  NOT NULL,
    [RecordActive] bit  NOT NULL,
    [Created] datetime  NOT NULL,
    [CreatedBy] nvarchar(512)  NOT NULL,
    [Updated] datetime  NULL,
    [UpdatedBy] nvarchar(512)  NULL
);
GO

-- Creating table 'UserGroups'
CREATE TABLE [dbo].[UserGroups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(256)  NOT NULL,
    [Description] nvarchar(512)  NULL,
    [RecordOrder] int  NOT NULL,
    [RecordActive] bit  NOT NULL,
    [Created] datetime  NOT NULL,
    [CreatedBy] nvarchar(512)  NOT NULL,
    [Updated] datetime  NULL,
    [UpdatedBy] nvarchar(512)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Email] nvarchar(256)  NOT NULL,
    [Password] nvarchar(256)  NOT NULL,
    [FirstName] nvarchar(256)  NOT NULL,
    [LastName] nvarchar(256)  NOT NULL,
    [Phone] nvarchar(50)  NOT NULL,
    [IdentityNumber] nvarchar(50)  NULL,
    [ImageFileName] nvarchar(512)  NULL,
    [RoleId] int  NOT NULL,
    [StatusId] int  NOT NULL,
    [LastLogin] datetime  NULL,
    [RecordOrder] int  NOT NULL,
    [RecordActive] bit  NOT NULL,
    [Created] datetime  NOT NULL,
    [CreatedBy] nvarchar(512)  NOT NULL,
    [Updated] datetime  NULL,
    [UpdatedBy] nvarchar(512)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'BookingRequests'
ALTER TABLE [dbo].[BookingRequests]
ADD CONSTRAINT [PK_BookingRequests]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GroupMenus'
ALTER TABLE [dbo].[GroupMenus]
ADD CONSTRAINT [PK_GroupMenus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Menus'
ALTER TABLE [dbo].[Menus]
ADD CONSTRAINT [PK_Menus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'News'
ALTER TABLE [dbo].[News]
ADD CONSTRAINT [PK_News]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NewsCategories'
ALTER TABLE [dbo].[NewsCategories]
ADD CONSTRAINT [PK_NewsCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NewsNewsCategories'
ALTER TABLE [dbo].[NewsNewsCategories]
ADD CONSTRAINT [PK_NewsNewsCategories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Pages'
ALTER TABLE [dbo].[Pages]
ADD CONSTRAINT [PK_Pages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Resources'
ALTER TABLE [dbo].[Resources]
ADD CONSTRAINT [PK_Resources]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [PK_Rooms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RoomTypes'
ALTER TABLE [dbo].[RoomTypes]
ADD CONSTRAINT [PK_RoomTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SiteSettings'
ALTER TABLE [dbo].[SiteSettings]
ADD CONSTRAINT [PK_SiteSettings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Status'
ALTER TABLE [dbo].[Status]
ADD CONSTRAINT [PK_Status]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserGroups'
ALTER TABLE [dbo].[UserGroups]
ADD CONSTRAINT [PK_UserGroups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CountryId] in table 'Resources'
ALTER TABLE [dbo].[Resources]
ADD CONSTRAINT [FK_Resources_Countries]
    FOREIGN KEY ([CountryId])
    REFERENCES [dbo].[Countries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Resources_Countries'
CREATE INDEX [IX_FK_Resources_Countries]
ON [dbo].[Resources]
    ([CountryId]);
GO

-- Creating foreign key on [MenuId] in table 'GroupMenus'
ALTER TABLE [dbo].[GroupMenus]
ADD CONSTRAINT [FK_RoleMenu_Menu]
    FOREIGN KEY ([MenuId])
    REFERENCES [dbo].[Menus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleMenu_Menu'
CREATE INDEX [IX_FK_RoleMenu_Menu]
ON [dbo].[GroupMenus]
    ([MenuId]);
GO

-- Creating foreign key on [UserGroupId] in table 'GroupMenus'
ALTER TABLE [dbo].[GroupMenus]
ADD CONSTRAINT [FK_RoleMenu_Role]
    FOREIGN KEY ([UserGroupId])
    REFERENCES [dbo].[UserGroups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleMenu_Role'
CREATE INDEX [IX_FK_RoleMenu_Role]
ON [dbo].[GroupMenus]
    ([UserGroupId]);
GO

-- Creating foreign key on [ParentId] in table 'Menus'
ALTER TABLE [dbo].[Menus]
ADD CONSTRAINT [FK_Menu_Menu]
    FOREIGN KEY ([ParentId])
    REFERENCES [dbo].[Menus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Menu_Menu'
CREATE INDEX [IX_FK_Menu_Menu]
ON [dbo].[Menus]
    ([ParentId]);
GO

-- Creating foreign key on [NewsId] in table 'NewsNewsCategories'
ALTER TABLE [dbo].[NewsNewsCategories]
ADD CONSTRAINT [FK_NewsNewsCategory_News]
    FOREIGN KEY ([NewsId])
    REFERENCES [dbo].[News]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NewsNewsCategory_News'
CREATE INDEX [IX_FK_NewsNewsCategory_News]
ON [dbo].[NewsNewsCategories]
    ([NewsId]);
GO

-- Creating foreign key on [NewsCategoryId] in table 'NewsNewsCategories'
ALTER TABLE [dbo].[NewsNewsCategories]
ADD CONSTRAINT [FK_NewsNewsCategory_NewsCategory]
    FOREIGN KEY ([NewsCategoryId])
    REFERENCES [dbo].[NewsCategories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_NewsNewsCategory_NewsCategory'
CREATE INDEX [IX_FK_NewsNewsCategory_NewsCategory]
ON [dbo].[NewsNewsCategories]
    ([NewsCategoryId]);
GO

-- Creating foreign key on [RoomTypeId] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [FK_Room_Room]
    FOREIGN KEY ([RoomTypeId])
    REFERENCES [dbo].[RoomTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Room_Room'
CREATE INDEX [IX_FK_Room_Room]
ON [dbo].[Rooms]
    ([RoomTypeId]);
GO

-- Creating foreign key on [StatusId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_User_Status]
    FOREIGN KEY ([StatusId])
    REFERENCES [dbo].[Status]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_User_Status'
CREATE INDEX [IX_FK_User_Status]
ON [dbo].[Users]
    ([StatusId]);
GO

-- Creating foreign key on [RoleId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_User_Role]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[UserGroups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_User_Role'
CREATE INDEX [IX_FK_User_Role]
ON [dbo].[Users]
    ([RoleId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------