USE [PXHotel]
GO
/****** Object:  Table [dbo].[BookingRequests]    Script Date: 3/24/2014 5:52:08 AM ******/
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
/****** Object:  Table [dbo].[Countries]    Script Date: 3/24/2014 5:52:09 AM ******/
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
/****** Object:  Table [dbo].[GroupPermissions]    Script Date: 3/24/2014 5:52:09 AM ******/
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
/****** Object:  Table [dbo].[Languages]    Script Date: 3/24/2014 5:52:09 AM ******/
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
/****** Object:  Table [dbo].[LocalizedResources]    Script Date: 3/24/2014 5:52:09 AM ******/
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
/****** Object:  Table [dbo].[Menus]    Script Date: 3/24/2014 5:52:09 AM ******/
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
	[Permissions] [nvarchar](max) NULL,
	[Visible] [bit] NOT NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[News]    Script Date: 3/24/2014 5:52:09 AM ******/
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
/****** Object:  Table [dbo].[NewsCategories]    Script Date: 3/24/2014 5:52:09 AM ******/
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
/****** Object:  Table [dbo].[NewsNewsCategories]    Script Date: 3/24/2014 5:52:09 AM ******/
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
/****** Object:  Table [dbo].[Pages]    Script Date: 3/24/2014 5:52:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](512) NOT NULL,
	[Caption] [nvarchar](max) NOT NULL,
	[CaptionWorking] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NOT NULL,
	[ContentWorking] [nvarchar](max) NULL,
	[FriendlyUrl] [nvarchar](512) NOT NULL,
	[PageTemplateId] [int] NULL,
	[Status] [int] NOT NULL,
	[Hierarchy] [nvarchar](max) NOT NULL,
	[ParentId] [int] NULL,
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
/****** Object:  Table [dbo].[PageTemplates]    Script Date: 3/24/2014 5:52:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageTemplates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Hierarchy] [nvarchar](max) NOT NULL,
	[ParentId] [int] NULL,
	[RecordOrder] [int] NOT NULL,
	[RecordActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](512) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](512) NULL,
 CONSTRAINT [PK_PageTemplates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 3/24/2014 5:52:09 AM ******/
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
/****** Object:  Table [dbo].[RoomTypes]    Script Date: 3/24/2014 5:52:09 AM ******/
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
/****** Object:  Table [dbo].[SettingTypes]    Script Date: 3/24/2014 5:52:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SettingTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[RecordOder] [int] NOT NULL,
	[RecordActive] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](512) NOT NULL,
	[Updated] [datetime] NULL,
	[UpdatedBy] [nvarchar](512) NULL,
 CONSTRAINT [PK_SettingTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SiteSettings]    Script Date: 3/24/2014 5:52:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](512) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[SettingTypeId] [int] NULL,
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
/****** Object:  Table [dbo].[UserGroups]    Script Date: 3/24/2014 5:52:09 AM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 3/24/2014 5:52:09 AM ******/
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
	[BirthDay] [datetime] NULL,
	[About] [nvarchar](max) NULL,
	[AvatarFileName] [nvarchar](512) NULL,
	[Address] [nvarchar](1024) NULL,
	[UserGroupId] [int] NOT NULL,
	[Status] [int] NOT NULL,
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
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[GroupPermissions] ON 

GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (1, 1, 1, 1)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (2, 1, 2, 1)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (3, 1, 3, 1)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (4, 2, 1, 0)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (5, 2, 2, 1)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (6, 2, 3, 0)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (7, 2, 4, 1)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (8, 1, 4, 1)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (1002, 3, 1, 0)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (1003, 3, 2, 0)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (1004, 3, 3, 0)
GO
INSERT [dbo].[GroupPermissions] ([Id], [UserGroupId], [PermissionId], [HasPermission]) VALUES (1005, 3, 4, 0)
GO
SET IDENTITY_INSERT [dbo].[GroupPermissions] OFF
GO
INSERT [dbo].[Languages] ([Id], [Name], [ShortName], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (N'en-US', N'English', N'America', 1, 1, CAST(0x0000A2F4010152B3 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F40101A8B8 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Languages] ([Id], [Name], [ShortName], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (N'vi-VN', N'VietNam', N'VietNam', 2, 1, CAST(0x0000A2F4010152B3 AS DateTime), N'admninistrator', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[LocalizedResources] ON 

GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'en-US', N'test2', N'test', N'test 1 2', 0, 0, CAST(0x0000A2F40104D52E AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F60046EF08 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'en-US', N'Upload Successfully.', N'Upload Successfully.', N'Upload Successfully.', 0, 0, CAST(0x0000A2F4015D8EDF AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3, N'en-US', N'My Profiles', N'My Profiles', N'My Profiles', 0, 0, CAST(0x0000A2F50028317D AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (4, N'en-US', N'Manage my profile', N'Manage my profile', N'Manage my profile', 0, 0, CAST(0x0000A2F500283313 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1002, N'en-US', N'AdminModule:::LocalizedResources:::Update localized resource successfully', N'Update localized resource successfully', N'Update localized resource successfully', 0, 0, CAST(0x0000A2F6000F557C AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1003, N'en-US', N'AdminModule:::Users:::ValidationMessage:::Email is existed.', N'Email is existed.', N'Email is existed.', 0, 0, CAST(0x0000A2F6001B7738 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1004, N'en-US', N'Validation Error', N'Validation Error', N'Validation Error', 0, 0, CAST(0x0000A2F6001B8008 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1005, N'en-US', N'AdminModule:::Users:::Update user successfully', N'Update user successfully', N'Update user successfully', 0, 0, CAST(0x0000A2F600282174 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1006, N'en-US', N'AdminModule:::Users:::Login succesfully', N'Login succesfully', N'Login succesfully', 0, 0, CAST(0x0000A2F6003A7883 AS DateTime), N'', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1007, N'en-US', N'AdminModule:::Users:::Message:::Login successfully', N'Login successfully', N'Login successfully', 0, 0, CAST(0x0000A2F6003A788F AS DateTime), N'', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1008, N'en-US', N'AdminModule:::Users:::Login successfully1', N'Login successfully1', N'Login successfully1', 0, 0, CAST(0x0000A2F6003CC92A AS DateTime), N'', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1010, N'en-US', N'AdminModule:::LocalizedResources:::Insert localized resource successfully', N'Insert localized resource successfully', N'Insert localized resource successfully', 0, 0, CAST(0x0000A2F6004959CB AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1011, N'en-US', N'AdminModule:::LocalizedResources:::Delete localized resource successfully', N'Delete localized resource successfully', N'Delete localized resource successfully', 0, 0, CAST(0x0000A2F6004A1717 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2002, N'en-US', N'AdminModule:::Users:::Change password successfully', N'Change password successfully', N'Change password successfully', 0, 0, CAST(0x0000A2F600DEFF61 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2003, N'en-US', N'AdminModule:::Users:::ValidationMessage:::Wrong old password.', N'Wrong old password.', N'Wrong old password.', 0, 0, CAST(0x0000A2F600DF0AA7 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2004, N'en-US', N'AdminModule:::Users:::Update user data successfully', N'Update user data successfully', N'Update user data successfully', 0, 0, CAST(0x0000A2F600FCF3C5 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2005, N'en-US', N'AdminModule:::Users:::Upload avatar successfully', N'Upload avatar successfully', N'Upload avatar successfully', 0, 0, CAST(0x0000A2F600FE4D40 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2022, N'vi-VN', N'test2', N'test', N'test 123 123 12 1
', 0, 0, CAST(0x0000A2F6013AB775 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F60142E0C4 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2023, N'vi-VN', N'AdminModule:::LocalizedResources:::Update localized resource successfully', N'Update localized resource successfully', N'Update localized resource successfully', 0, 0, CAST(0x0000A2F60142E0CB AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2024, N'vi-VN', N'test1231231', N'test', N'test', 0, 0, CAST(0x0000A2F60142FC58 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2025, N'vi-VN', N'AdminModule:::LocalizedResources:::Insert localized resource successfully', N'Insert localized resource successfully', N'Insert localized resource successfully', 0, 0, CAST(0x0000A2F60142FC69 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2026, N'vi-VN', N'My Profiles', N'My Profiles', N'My Profiles', 0, 0, CAST(0x0000A2F601437013 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2027, N'vi-VN', N'Manage my profile', N'Manage my profile', N'Manage my profile', 0, 0, CAST(0x0000A2F601437019 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2028, N'en-US', N'test1231231', N'test1231231', N'test1231231', 0, 0, CAST(0x0000A2F60148F182 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2029, N'en-US', N'AdminModule:::Menus:::Create menu successfully', N'Create menu successfully', N'Create menu successfully', 0, 0, CAST(0x0000A2F70011B4AF AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2030, N'vi-VN', N'AdminModule:::Menus:::Create menu successfully', N'Create menu successfully', N'Create menu successfully', 0, 0, CAST(0x0000A2F70011B4B4 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2031, N'en-US', N'AdminModule:::Menus:::Delete menu successfully', N'Delete menu successfully', N'Delete menu successfully', 0, 0, CAST(0x0000A2F70011CC71 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2032, N'vi-VN', N'AdminModule:::Menus:::Delete menu successfully', N'Delete menu successfully', N'Delete menu successfully', 0, 0, CAST(0x0000A2F70011CC72 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2033, N'en-US', N'AdminModule:::Pages:::Insert page successfully', N'Insert page successfully', N'Insert page successfully', 0, 0, CAST(0x0000A2F70014483E AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2034, N'vi-VN', N'AdminModule:::Pages:::Insert page successfully', N'Insert page successfully', N'Insert page successfully', 0, 0, CAST(0x0000A2F700144843 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2035, N'en-US', N'AdminModule:::Pages:::Update page successfully', N'Update page successfully', N'Update page successfully', 0, 0, CAST(0x0000A2F700291F5D AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2036, N'vi-VN', N'AdminModule:::Pages:::Update page successfully', N'Update page successfully', N'Update page successfully', 0, 0, CAST(0x0000A2F700291F62 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2037, N'en-US', N'AdminModule:::Users:::Insert user successfully', N'Insert user successfully', N'Insert user successfully', 0, 0, CAST(0x0000A2F700315FEF AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2038, N'vi-VN', N'AdminModule:::Users:::Insert user successfully', N'Insert user successfully', N'Insert user successfully', 0, 0, CAST(0x0000A2F700315FF2 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2039, N'en-US', N'AdminModule:::PageTemplates:::Insert page template successfully', N'Insert page template successfully', N'Insert page template successfully', 0, 0, CAST(0x0000A2F700367D91 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2040, N'vi-VN', N'AdminModule:::PageTemplates:::Insert page template successfully', N'Insert page template successfully', N'Insert page template successfully', 0, 0, CAST(0x0000A2F700367D95 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2041, N'en-US', N'AdminModule:::PageTemplates:::Delete page template successfully', N'Delete page template successfully', N'Delete page template successfully', 0, 0, CAST(0x0000A2F70036A03E AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2042, N'vi-VN', N'AdminModule:::PageTemplates:::Delete page template successfully', N'Delete page template successfully', N'Delete page template successfully', 0, 0, CAST(0x0000A2F70036A03F AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2043, N'en-US', N'AdminModule:::PageTemplates:::Update page template successfully', N'Update page template successfully', N'Update page template successfully', 0, 0, CAST(0x0000A2F70037D1B3 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2044, N'vi-VN', N'AdminModule:::PageTemplates:::Update page template successfully', N'Update page template successfully', N'Update page template successfully', 0, 0, CAST(0x0000A2F70037D1BC AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3002, N'en-US', N'AdminModule:::PageTemplates:::Form:::Name', N'Name', N'Name', 0, 0, CAST(0x0000A2F80006FC73 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3003, N'vi-VN', N'AdminModule:::PageTemplates:::Form:::Name', N'Name', N'Name', 0, 0, CAST(0x0000A2F80006FCC5 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3004, N'en-US', N'AdminModule:::PageTemplates:::Form:::Name Placeholder', N'Name Placeholder', N'Name Placeholder', 0, 0, CAST(0x0000A2F80006FCC8 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3005, N'vi-VN', N'AdminModule:::PageTemplates:::Form:::Name Placeholder', N'Name Placeholder', N'Name Placeholder', 0, 0, CAST(0x0000A2F80006FCC9 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3006, N'en-US', N'AdminModule:::PageTemplates:::Forms:::Name', N'Name', N'Name', 0, 0, CAST(0x0000A2F80007F495 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3007, N'vi-VN', N'AdminModule:::PageTemplates:::Forms:::Name', N'Name', N'Name', 0, 0, CAST(0x0000A2F80007F49A AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3008, N'en-US', N'AdminModule:::PageTemplates:::Forms:::Content', N'Content', N'Content', 0, 0, CAST(0x0000A2F80007F49E AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3009, N'vi-VN', N'AdminModule:::PageTemplates:::Forms:::Content', N'Content', N'Content', 0, 0, CAST(0x0000A2F80007F49F AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3010, N'en-US', N'AdminModule:::PageTemplates:::Forms:::Parent Template Dropdown', N'Parent', N'Parent', 0, 0, CAST(0x0000A2F8000A81FD AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3011, N'vi-VN', N'AdminModule:::PageTemplates:::Forms:::Parent Template Dropdown', N'Parent', N'Parent', 0, 0, CAST(0x0000A2F8000A8203 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3012, N'en-US', N'AdminModule:::PageTemplates:::ValidationMessage:::Missing {RenderBody} Curly Bracket.', N'Missing {RenderBody} Curly Bracket.', N'Missing {RenderBody} Curly Bracket.', 0, 0, CAST(0x0000A2F8001B4FC2 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3013, N'vi-VN', N'AdminModule:::PageTemplates:::ValidationMessage:::Missing {RenderBody} Curly Bracket.', N'Missing {RenderBody} Curly Bracket.', N'Missing {RenderBody} Curly Bracket.', 0, 0, CAST(0x0000A2F8001B4FCC AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3014, N'en-US', N'AdminModule:::PageTemplates:::Forms:::AddCurlyBracket', N'Add curly bracket', N'Add curly bracket', 0, 0, CAST(0x0000A2F800219243 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[LocalizedResources] ([Id], [LanguageId], [TextKey], [DefaultValue], [TranslatedValue], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3015, N'vi-VN', N'AdminModule:::PageTemplates:::Forms:::AddCurlyBracket', N'Add curly bracket', N'Add curly bracket', 0, 0, CAST(0x0000A2F80021924E AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[LocalizedResources] OFF
GO
SET IDENTITY_INSERT [dbo].[Menus] ON 

GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (1, N'Dashboards', NULL, N'Home', N'Index', NULL, N'.00001.', N'icon-adjust', 1, 1, CAST(0x0000A2F3017D395F AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F8005D37BE AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (2, N'Contents', NULL, NULL, NULL, NULL, N'.00002.', N'icon-adjust', 1, 2, CAST(0x0000A2F3017D83A3 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F8005D37DE AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (3, N'Settings', NULL, NULL, NULL, NULL, N'.00003.', N'icon-adjust', 1, 3, CAST(0x0000A2F3017DAD77 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F8005D37DF AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (4, N'Users', NULL, NULL, NULL, NULL, N'.00004.', N'icon-adjust', 1, 4, CAST(0x0000A2F3017DEEC3 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F8005D37DF AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (5, N'Bookings', NULL, NULL, NULL, NULL, N'.00005.', N'icon-adjust', 1, 5, CAST(0x0000A2F3017E07CA AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F8005D37E0 AS DateTime), N'levanvunam@gmail.com', N'', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (6, N'Site Settings', NULL, N'SiteSettings', N'Index', 3, N'.00003.00006.', N'icon-adjust', 1, 1, CAST(0x0000A2F3017EC04C AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F8005D37E5 AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (7, N'Menus', NULL, N'Menus', N'Index', 2, N'.00002.00007.', N'icon-adjust', 1, 2, CAST(0x0000A2F3017EDD21 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F8005D37E8 AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (8, N'Pages', NULL, N'Pages', N'Index', 2, N'.00002.00008.', N'icon-adjust', 1, 1, CAST(0x0000A2F3017EF5F2 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F8005D37ED AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (9, N'User List', NULL, N'Users', N'Index', 4, N'.00004.00009.', N'icon-adjust', 1, 1, CAST(0x0000A2F301881DB3 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F8005D37F1 AS DateTime), N'levanvunam@gmail.com', N'2', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (10, N'User Groups', NULL, N'UserGroups', N'Index', 4, N'.00004.00010.', N'icon-adjust', 1, 2, CAST(0x0000A2F3018835E9 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F8005D37F5 AS DateTime), N'levanvunam@gmail.com', N'2', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (11, N'Permissions', NULL, N'UserGroups', N'Permissions', 10, N'.00004.00010.00011.', N'icon-adjust', 1, 3, CAST(0x0000A2F3018889E5 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F8005D37F9 AS DateTime), N'levanvunam@gmail.com', N'2', 0)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (12, N'Languages', NULL, N'Languages', N'Index', 2, N'.00002.00012.', N'icon-adjust', 1, 3, CAST(0x0000A2F400031B82 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F8005D37FD AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
INSERT [dbo].[Menus] ([Id], [Name], [Url], [Controller], [Action], [ParentId], [Hierarchy], [MenuIcon], [RecordActive], [RecordOrder], [Created], [CreatedBy], [Updated], [UpdatedBy], [Permissions], [Visible]) VALUES (14, N'Page Templates', NULL, N'PageTemplates', N'Index', 8, N'.00002.00008.00014.', N'icon-adjust', 1, 1, CAST(0x0000A2F700347D91 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F8005D3801 AS DateTime), N'levanvunam@gmail.com', N'4', 1)
GO
SET IDENTITY_INSERT [dbo].[Menus] OFF
GO
SET IDENTITY_INSERT [dbo].[Pages] ON 

GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [Status], [Hierarchy], [ParentId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'test', N'', NULL, N'123123 131 23123 ', NULL, N'test', 4, 1, N'.00001.', NULL, 1, 1, CAST(0x0000A2F700144793 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F7001447EE AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Pages] ([Id], [Title], [Caption], [CaptionWorking], [Content], [ContentWorking], [FriendlyUrl], [PageTemplateId], [Status], [Hierarchy], [ParentId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'test2', N'', NULL, N'asdasdasdasdadasd', NULL, N'test22', 4, 2, N'.00002.', NULL, 2, 1, CAST(0x0000A2F7001470F3 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F701053403 AS DateTime), N'levanvunam@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[Pages] OFF
GO
SET IDENTITY_INSERT [dbo].[PageTemplates] ON 

GO
INSERT [dbo].[PageTemplates] ([Id], [Name], [Content], [Hierarchy], [ParentId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'test', N'First Level - {RenderBody}', N'.00002.', NULL, 0, 1, CAST(0x0000A2F70036DCC2 AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F70036DCE8 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[PageTemplates] ([Id], [Name], [Content], [Hierarchy], [ParentId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3, N'test1', N'Second Level - {RenderBody} 2{Page}

{Page}', N'.00002.00003.', 2, 0, 1, CAST(0x0000A2F70036E48C AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F8002F0DFF AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[PageTemplates] ([Id], [Name], [Content], [Hierarchy], [ParentId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (4, N'test 222222', N'4 Level - {RenderBody}', N'.00002.00003.00005.00004.', 5, 0, 1, CAST(0x0000A2F70036EACE AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F70040D31E AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[PageTemplates] ([Id], [Name], [Content], [Hierarchy], [ParentId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (5, N'test22', N'3 Level - {RenderBody}', N'.00002.00003.00005.', 3, 0, 1, CAST(0x0000A2F70036FE3B AS DateTime), N'levanvunam@gmail.com', CAST(0x0000A2F70037D1A0 AS DateTime), N'levanvunam@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[PageTemplates] OFF
GO
SET IDENTITY_INSERT [dbo].[SettingTypes] ON 

GO
INSERT [dbo].[SettingTypes] ([Id], [Name], [RecordOder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'System', 1, 1, CAST(0x0000A2F3017F57F8 AS DateTime), N'administrator', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[SettingTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[SiteSettings] ON 

GO
INSERT [dbo].[SiteSettings] ([Id], [Name], [Value], [SettingTypeId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'Default Admin', N'administrator', 1, 1, 1, CAST(0x0000A2F3017F57F8 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[SiteSettings] ([Id], [Name], [Value], [SettingTypeId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'MaxSizeUploaded', N'10485760', NULL, 2, 1, CAST(0x0000A2F4015A232D AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[SiteSettings] ([Id], [Name], [Value], [SettingTypeId], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1002, N'PasswordSetting', N'{"PasswordMinLengthRequired":0,"PasswordMustHaveUpperAndLowerCaseLetters":false,"PasswordMustHaveDigit":false,"PasswordMustHaveSymbol":false}', NULL, 0, 1, CAST(0x0000A2F7010C4BB2 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[SiteSettings] OFF
GO
SET IDENTITY_INSERT [dbo].[UserGroups] ON 

GO
INSERT [dbo].[UserGroups] ([Id], [Name], [Description], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (1, N'Administrator', N'Administrator', 1, 1, CAST(0x0000A2F3017B0740 AS DateTime), N'administrator', NULL, NULL)
GO
INSERT [dbo].[UserGroups] ([Id], [Name], [Description], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'Moderator', N'Moderator', 2, 1, CAST(0x0000A2F30188E332 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
INSERT [dbo].[UserGroups] ([Id], [Name], [Description], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3, N'Customer', N'Customer', 3, 1, CAST(0x0000A2F30188FB75 AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[UserGroups] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [FirstName], [LastName], [Phone], [IdentityNumber], [BirthDay], [About], [AvatarFileName], [Address], [UserGroupId], [Status], [LastLogin], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (2, N'levanvunam@gmail.com', N'admin', N'Nam', N'Le', N'098988733 2', N'273390999', CAST(0x0000A2C000000000 AS DateTime), N'123123 12 312 12 3123 12 1 221 3123 123', N'2_20140322095152.jpg', N'cầu Trần Quang Diệu, Ho Chi Minh City, Vietnam', 1, 2, CAST(0x0000A2F8005D3845 AS DateTime), 0, 1, CAST(0x0000A2F3017B0740 AS DateTime), N'administrator', CAST(0x0000A2F8005D3845 AS DateTime), N'levanvunam@gmail.com')
GO
INSERT [dbo].[Users] ([Id], [Email], [Password], [FirstName], [LastName], [Phone], [IdentityNumber], [BirthDay], [About], [AvatarFileName], [Address], [UserGroupId], [Status], [LastLogin], [RecordOrder], [RecordActive], [Created], [CreatedBy], [Updated], [UpdatedBy]) VALUES (3, N'nam.le@saigontechnology.vn', N'123', N'123', N'123', N'123', N'123', NULL, NULL, NULL, NULL, 1, 1, NULL, 0, 1, CAST(0x0000A2F700315FCF AS DateTime), N'levanvunam@gmail.com', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
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
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Pages] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Pages] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_Pages]
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_PageTemplates] FOREIGN KEY([PageTemplateId])
REFERENCES [dbo].[PageTemplates] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_PageTemplates]
GO
ALTER TABLE [dbo].[PageTemplates]  WITH CHECK ADD  CONSTRAINT [FK_PageTemplates_PageTemplates] FOREIGN KEY([ParentId])
REFERENCES [dbo].[PageTemplates] ([Id])
GO
ALTER TABLE [dbo].[PageTemplates] CHECK CONSTRAINT [FK_PageTemplates_PageTemplates]
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
