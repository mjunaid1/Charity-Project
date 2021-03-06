USE [Charity_DB]
GO
/****** Object:  Table [dbo].[Login_tbl]    Script Date: 5/10/2018 6:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login_tbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](255) NULL,
	[Created] [datetime] NULL,
	[role] [int] NULL,
 CONSTRAINT [PK_Login_tbl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stories_tbl]    Script Date: 5/10/2018 6:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stories_tbl](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[VideoLink] [nvarchar](255) NULL,
	[ImageLink] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Stories_tbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Login_tbl] ON 

INSERT [dbo].[Login_tbl] ([id], [Name], [Username], [Password], [Created], [role]) VALUES (1, N'Admin', N'admin@gmail.com', N'admin123', NULL, 1)
SET IDENTITY_INSERT [dbo].[Login_tbl] OFF
SET IDENTITY_INSERT [dbo].[Stories_tbl] ON 

INSERT [dbo].[Stories_tbl] ([Id], [Title], [VideoLink], [ImageLink], [Description]) VALUES (8, N'case 1', N'AngularJS anchorscroll example - YouTube_6573.MP4', N'Untitled2222_6573.png', N'sadfasfafasf')
SET IDENTITY_INSERT [dbo].[Stories_tbl] OFF
