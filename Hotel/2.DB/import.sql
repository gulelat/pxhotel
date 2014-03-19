USE [master]
GO
/****** Object:  Database [PXHotel]    Script Date: 3/20/2014 4:49:36 AM ******/
CREATE DATABASE [PXHotel]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PxHotel', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\PxHotel.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PxHotel_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\PxHotel_log.ldf' , SIZE = 3456KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PXHotel] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PXHotel].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PXHotel] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PXHotel] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PXHotel] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PXHotel] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PXHotel] SET ARITHABORT OFF 
GO
ALTER DATABASE [PXHotel] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PXHotel] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [PXHotel] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PXHotel] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PXHotel] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PXHotel] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PXHotel] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PXHotel] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PXHotel] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PXHotel] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PXHotel] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PXHotel] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PXHotel] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PXHotel] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PXHotel] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PXHotel] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PXHotel] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PXHotel] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PXHotel] SET RECOVERY FULL 
GO
ALTER DATABASE [PXHotel] SET  MULTI_USER 
GO
ALTER DATABASE [PXHotel] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PXHotel] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PXHotel] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PXHotel] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PXHotel', N'ON'
GO
USE [PXHotel]
GO
/****** Object:  Table [dbo].[BookingRequests]    Script Date: 3/20/2014 4:49:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](512) NOT NULL,
	[Email] [nvarchar](512) NOT NULL,
	[IndentityNumber] [nvarchar](128) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Country] [nvarchar](512) NOT NULL,
	[Phone] [nvarchar](512) NOT NULL,
	[Note] [nvarchar](max) NULL,
	[TotalRooms] [int] NOT NULL,
	[BookingFrom] [datetime] NOT NULL,
	[BookingTo] [datetime] NOT NULL,
	[RecordOrder] [int] NOT NULL,
	[RecordActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](512) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](512) NULL,
 CONSTRAINT [PK_BookingRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Countries]    Script Date: 3/20/2014 4:49:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[RecordOrder] [int] NOT NULL,
	[RecordActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](512) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](512) NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroupPermissions]    Script Date: 3/20/2014 4:49:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupPermissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserGroupId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
	[HasPermission] [bit] NOT NULL,
 CONSTRAINT [PK_GroupPermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Languages]    Script Date: 3/20/2014 4:49:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[Id] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[ShortName] [nvarchar](10) NOT NULL,
	[RecordOrder] [int] NOT NULL,
	[RecordActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](512) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](512) NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LocalizedResources]    Script Date: 3/20/2014 4:49:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalizedResources](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LanguageId] [nvarchar](10) NOT NULL,
	[TextKey] [nvarchar](max) NOT NULL,
	[DefaultValue] [nvarchar](max) NOT NULL,
	[TranslatedValue] [nvarchar](max) NOT NULL,
	[RecordOrder] [int] NOT NULL,
	[RecordActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](512) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](512) NULL,
 CONSTRAINT [PK_LocalizedResources] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menus]    Script Date: 3/20/2014 4:49:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Url] [nvarchar](512) NULL,
	[Controller] [nvarchar](512) NULL,
	[Action] [nvarchar](512) NULL,
	[ParentId] [int] NULL,
	[Hierarchy] [nvarchar](max) NOT NULL,
	[MenuIcon] [nvarchar](512) NULL,
	[RecordActive] [bit] NOT NULL,
	[RecordOrder] [int] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](512) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](512) NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[News]    Script Date: 3/20/2014 4:49:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[ImageFileName] [nvarchar](512) NULL,
	[Status] [int] NOT NULL,
	[RecordOrder] [int] NOT NULL,
	[RecordActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](512) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](512) NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NewsCategories]    Script Date: 3/20/2014 4:49:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[RecordOrder] [int] NOT NULL,
	[RecordActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](512) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](512) NULL,
 CONSTRAINT [PK_NewsCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NewsNewsCategories]    Script Date: 3/20/2014 4:49:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsNewsCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NewsId] [int] NOT NULL,
	[NewsCategoryId] [int] NOT NULL,
 CONSTRAINT [PK_NewsNewsCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pages]    Script Date: 3/20/2014 4:49:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](512) NOT NULL,
	[Caption] [nvarchar](max) NULL,
	[Caption_Working] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Content_Working] [nvarchar](max) NOT NULL,
	[StatusId] [int] NOT NULL,
	[FriendlyUrl] [nvarchar](512) NOT NULL,
	[RecordOrder] [int] NOT NULL,
	[RecordActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](512) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](512) NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Resources]    Script Date: 3/20/2014 4:49:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resources](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[DefaultValue] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[CountryId] [int] NOT NULL,
	[RecordOrder] [int] NOT NULL,
	[RecordActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](512) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](512) NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 3/20/2014 4:49:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[Note] [nvarchar](max) NULL,
	[StatusId] [int] NOT NULL,
	[RoomTypeId] [int] NOT NULL,
	[RecordOrder] [int] NOT NULL,
	[RecordActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](512) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](512) NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoomTypes]    Script Date: 3/20/2014 4:49:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [float] NOT NULL,
	[RecordOrder] [int] NOT NULL,
	[RecordActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedDate] [nvarchar](512) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](512) NULL,
 CONSTRAINT [PK_RoomTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SiteSettings]    Script Date: 3/20/2014 4:49:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[RecordOrder] [int] NOT NULL,
	[RecordActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](512) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](512) NULL,
 CONSTRAINT [PK_SiteSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Status]    Script Date: 3/20/2014 4:49:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[RecordOrder] [int] NOT NULL,
	[RecordActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](512) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](512) NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserGroups]    Script Date: 3/20/2014 4:49:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[RecordOrder] [int] NOT NULL,
	[RecordActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](512) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](512) NULL,
 CONSTRAINT [PK_UserGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/20/2014 4:49:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
	[FirstName] [nvarchar](256) NOT NULL,
	[LastName] [nvarchar](256) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[IdentityNumber] [nvarchar](50) NULL,
	[ImageFileName] [nvarchar](512) NULL,
	[UserGroupId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[LastLogin] [datetime] NULL,
	[RecordOrder] [int] NOT NULL,
	[RecordActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](512) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](512) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[GroupPermissions] ON 

INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (1, 1, 1, 1)
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (2, 1, 2, 1)
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (3, 1, 3, 1)
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (4, 2, 1, 1)
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (5, 2, 2, 0)
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (6, 2, 3, 1)
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (7, 2, 4, 0)
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (8, 1, 4, 1)
SET IDENTITY_INSERT [dbo].[GroupPermissions] OFF
SET IDENTITY_INSERT [dbo].[Menus] ON 

INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'Dashboards', NULL, N'Home', N'Index', NULL, N'.00001.', N'icon-adjust', 1, 1, CAST(0x0000A2F3017D395F AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F3017D39A1 AS DateTime), N'levanvunam@gmail.com')
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'Contents', NULL, NULL, NULL, NULL, N'.00002.', N'icon-adjust', 1, 2, CAST(0x0000A2F3017D83A3 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F3017E2C84 AS DateTime), N'levanvunam@gmail.com')
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3, N'Settings', NULL, NULL, NULL, NULL, N'.00003.', N'icon-adjust', 1, 3, CAST(0x0000A2F3017DAD77 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F3017DAD79 AS DateTime), N'levanvunam@gmail.com')
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (4, N'Users', NULL, NULL, NULL, NULL, N'.00004.', N'icon-adjust', 1, 4, CAST(0x0000A2F3017DEEC3 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F3017DEED3 AS DateTime), N'levanvunam@gmail.com')
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (5, N'Bookings', NULL, NULL, NULL, NULL, N'.00005.', N'icon-adjust', 1, 5, CAST(0x0000A2F3017E07CA AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F3017E07DB AS DateTime), N'levanvunam@gmail.com')
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (6, N'Site Settings', NULL, N'SiteSettings', N'Index', 3, N'.00003.00006.', N'icon-adjust', 1, 1, CAST(0x0000A2F3017EC04C AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F3017EC064 AS DateTime), N'levanvunam@gmail.com')
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (7, N'Menus', NULL, N'Menus', N'Index', 2, N'.00002.00007.', N'icon-adjust', 1, 2, CAST(0x0000A2F3017EDD21 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F3017EDD31 AS DateTime), N'levanvunam@gmail.com')
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (8, N'Pages', NULL, N'Pages', N'Index', 2, N'.00002.00008.', N'icon-adjust', 1, 1, CAST(0x0000A2F3017EF5F2 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F3017EF608 AS DateTime), N'levanvunam@gmail.com')
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (9, N'User List', NULL, N'Users', N'Index', 4, N'.00004.00009.', N'icon-adjust', 1, 1, CAST(0x0000A2F301881DB3 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F301881DC8 AS DateTime), N'levanvunam@gmail.com')
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (10, N'User Groups', NULL, N'UserGroups', N'Index', 4, N'.00004.00010.', N'icon-adjust', 1, 2, CAST(0x0000A2F3018835E9 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F3018835F8 AS DateTime), N'levanvunam@gmail.com')
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (11, N'Permissions', NULL, N'UserGroups', N'Permissions', NULL, N'.00011.', N'icon-adjust', 1, 3, CAST(0x0000A2F3018889E5 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F3018889F6 AS DateTime), N'levanvunam@gmail.com')
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (12, N'Languages', NULL, N'Languages', N'Index', 2, N'.00002.00012.', N'icon-adjust', 1, 3, CAST(0x0000A2F400031B82 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F400031BA7 AS DateTime), N'levanvunam@gmail.com')
SET IDENTITY_INSERT [dbo].[Menus] OFF
SET IDENTITY_INSERT [dbo].[SiteSettings] ON 

INSERT [dbo].[SiteSettings] ([Id], [Name], [Value], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'Default Admin', N'administrator', 1, 1, CAST(0x0000A2F3017F57F8 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
SET IDENTITY_INSERT [dbo].[SiteSettings] OFF
SET IDENTITY_INSERT [dbo].[Status] ON 

INSERT [dbo].[Status] ([Id], [Name], [Description], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'Active', N'Active', 1, 1, CAST(0x0000A2F3017B0740 AS DateTime), N'administrator', NULL, NULL)
INSERT [dbo].[Status] ([Id], [Name], [Description], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'Inactive', N'Inactive', 1, 1, CAST(0x0000A2F3017B0740 AS DateTime), N'administrator', NULL, NULL)
INSERT [dbo].[Status] ([Id], [Name], [Description], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3, N'Locked', N'Locked', 1, 1, CAST(0x0000A2F3017B0740 AS DateTime), N'administrator', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Status] OFF
SET IDENTITY_INSERT [dbo].[UserGroups] ON 

INSERT [dbo].[UserGroups] ([Id], [Name], [Description], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'Administrator', N'Administrator', 1, 1, CAST(0x0000A2F3017B0740 AS DateTime), N'administrator', NULL, NULL)
INSERT [dbo].[UserGroups] ([Id], [Name], [Description], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'Moderator', N'Moderator', 2, 1, CAST(0x0000A2F30188E332 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
INSERT [dbo].[UserGroups] ([Id], [Name], [Description], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3, N'Customer', N'Customer', 3, 1, CAST(0x0000A2F30188FB75 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserGroups] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Email], [Password], [FirstName], [LastName], [Phone], [IdentityNumber], [ImageFileName], [UserGroupId], [StatusId], [LastLogin], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'levanvunam@gmail.com', N'admin', N'Nam', N'Le', N'0989887330', N'273390999', NULL, 1, 1, CAST(0x0000A2F4004A59B3 AS DateTime), 0, 1, CAST(0x0000A2F3017B0740 AS DateTime), N'administrator', CAST(0x0000A2F4004A59B3 AS DateTime), N'levanvunam@gmail.com')
SET IDENTITY_INSERT [dbo].[Users] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_FK_LocalizedResources_Languages]    Script Date: 3/20/2014 4:49:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_LocalizedResources_Languages] ON [dbo].[LocalizedResources]
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Menu_Menu]    Script Date: 3/20/2014 4:49:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Menu_Menu] ON [dbo].[Menus]
(
	[ParentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_NewsNewsCategory_News]    Script Date: 3/20/2014 4:49:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_NewsNewsCategory_News] ON [dbo].[NewsNewsCategories]
(
	[NewsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_NewsNewsCategory_NewsCategory]    Script Date: 3/20/2014 4:49:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_NewsNewsCategory_NewsCategory] ON [dbo].[NewsNewsCategories]
(
	[NewsCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Room_Room]    Script Date: 3/20/2014 4:49:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Room_Room] ON [dbo].[Rooms]
(
	[RoomTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_User_Role]    Script Date: 3/20/2014 4:49:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_User_Role] ON [dbo].[Users]
(
	[UserGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_User_Status]    Script Date: 3/20/2014 4:49:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_FK_User_Status] ON [dbo].[Users]
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GroupPermissions]  WITH CHECK ADD  CONSTRAINT [FK_GroupPermissions_UserGroups] FOREIGN KEY([UserGroupId])
REFERENCES [dbo].[UserGroups] ([Id])
GO
ALTER TABLE [dbo].[GroupPermissions] CHECK CONSTRAINT [FK_GroupPermissions_UserGroups]
GO
ALTER TABLE [dbo].[LocalizedResources]  WITH CHECK ADD  CONSTRAINT [FK_LocalizedResources_Languages] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[LocalizedResources] CHECK CONSTRAINT [FK_LocalizedResources_Languages]
GO
ALTER TABLE [dbo].[Menus]  WITH CHECK ADD  CONSTRAINT [FK_Menu_Menu] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Menus] ([Id])
GO
ALTER TABLE [dbo].[Menus] CHECK CONSTRAINT [FK_Menu_Menu]
GO
ALTER TABLE [dbo].[NewsNewsCategories]  WITH CHECK ADD  CONSTRAINT [FK_NewsNewsCategory_News] FOREIGN KEY([NewsId])
REFERENCES [dbo].[News] ([Id])
GO
ALTER TABLE [dbo].[NewsNewsCategories] CHECK CONSTRAINT [FK_NewsNewsCategory_News]
GO
ALTER TABLE [dbo].[NewsNewsCategories]  WITH CHECK ADD  CONSTRAINT [FK_NewsNewsCategory_NewsCategory] FOREIGN KEY([NewsCategoryId])
REFERENCES [dbo].[NewsCategories] ([Id])
GO
ALTER TABLE [dbo].[NewsNewsCategories] CHECK CONSTRAINT [FK_NewsNewsCategory_NewsCategory]
GO
ALTER TABLE [dbo].[Resources]  WITH CHECK ADD  CONSTRAINT [FK_Resources_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[Resources] CHECK CONSTRAINT [FK_Resources_Countries]
GO
ALTER TABLE [dbo].[Rooms]  WITH CHECK ADD  CONSTRAINT [FK_Room_Room] FOREIGN KEY([RoomTypeId])
REFERENCES [dbo].[RoomTypes] ([Id])
GO
ALTER TABLE [dbo].[Rooms] CHECK CONSTRAINT [FK_Room_Room]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([UserGroupId])
REFERENCES [dbo].[UserGroups] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_User_Role]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_User_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_User_Status]
GO
USE [master]
GO
ALTER DATABASE [PXHotel] SET  READ_WRITE 
GO
