USE [master]
GO
/****** Object:  Database [VoteOnline]    Script Date: 2023/10/4 下午 11:56:18 ******/
CREATE DATABASE [VoteOnline]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VoteOnline', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\VoteOnline.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VoteOnline_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\VoteOnline_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [VoteOnline] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VoteOnline].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VoteOnline] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VoteOnline] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VoteOnline] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VoteOnline] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VoteOnline] SET ARITHABORT OFF 
GO
ALTER DATABASE [VoteOnline] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VoteOnline] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VoteOnline] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VoteOnline] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VoteOnline] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VoteOnline] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VoteOnline] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VoteOnline] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VoteOnline] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VoteOnline] SET  ENABLE_BROKER 
GO
ALTER DATABASE [VoteOnline] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VoteOnline] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VoteOnline] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VoteOnline] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VoteOnline] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VoteOnline] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VoteOnline] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VoteOnline] SET RECOVERY FULL 
GO
ALTER DATABASE [VoteOnline] SET  MULTI_USER 
GO
ALTER DATABASE [VoteOnline] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VoteOnline] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VoteOnline] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VoteOnline] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VoteOnline] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VoteOnline] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'VoteOnline', N'ON'
GO
ALTER DATABASE [VoteOnline] SET QUERY_STORE = OFF
GO
USE [VoteOnline]
GO
/****** Object:  Table [dbo].[VoteItems]    Script Date: 2023/10/4 下午 11:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoteItems](
	[VoteItemId] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[VoteItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VoteRecords]    Script Date: 2023/10/4 下午 11:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoteRecords](
	[VoteId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](10) NOT NULL,
	[VoteItemId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[VoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VoteItemCounts]    Script Date: 2023/10/4 下午 11:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VoteItemCounts] AS
SELECT
    VI.VoteItemId,
    VI.ItemName,
    COUNT(VR.VoteItemId) AS VoteCount
FROM
    VoteItems VI
LEFT JOIN
    VoteRecords VR ON VI.VoteItemId = VR.VoteItemId
GROUP BY
    VI.VoteItemId, VI.ItemName;
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2023/10/4 下午 11:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserName] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Users] ([UserName]) VALUES (N'Leo')
INSERT [dbo].[Users] ([UserName]) VALUES (N'Randy')
INSERT [dbo].[Users] ([UserName]) VALUES (N'RSY')
INSERT [dbo].[Users] ([UserName]) VALUES (N'Sandy')
GO
SET IDENTITY_INSERT [dbo].[VoteItems] ON 

INSERT [dbo].[VoteItems] ([VoteItemId], [ItemName]) VALUES (1, N'電腦')
INSERT [dbo].[VoteItems] ([VoteItemId], [ItemName]) VALUES (2, N'滑鼠')
INSERT [dbo].[VoteItems] ([VoteItemId], [ItemName]) VALUES (3, N'鍵盤')
INSERT [dbo].[VoteItems] ([VoteItemId], [ItemName]) VALUES (4, N'鍵盤')
INSERT [dbo].[VoteItems] ([VoteItemId], [ItemName]) VALUES (5, N'音響')
SET IDENTITY_INSERT [dbo].[VoteItems] OFF
GO
SET IDENTITY_INSERT [dbo].[VoteRecords] ON 

INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (1, N'Leo', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (2, N'Sandy', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (3, N'Sandy', 2)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (4, N'Randy', 2)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (5, N'RSY', 2)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (7, N'RSY', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (8, N'RSY', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (9, N'RSY', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (10, N'RSY', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (11, N'RSY', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (12, N'RSY', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (13, N'Randy', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (14, N'Randy', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (15, N'Randy', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (16, N'Randy', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (17, N'Leo', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (18, N'Leo', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (19, N'Leo', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (20, N'Leo', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (21, N'Leo', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (22, N'Leo', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (25, N'Leo', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (26, N'Leo', 1)
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (27, N'Randy', 1)
SET IDENTITY_INSERT [dbo].[VoteRecords] OFF
GO
ALTER TABLE [dbo].[VoteRecords]  WITH CHECK ADD FOREIGN KEY([UserName])
REFERENCES [dbo].[Users] ([UserName])
GO
ALTER TABLE [dbo].[VoteRecords]  WITH CHECK ADD FOREIGN KEY([VoteItemId])
REFERENCES [dbo].[VoteItems] ([VoteItemId])
GO
USE [master]
GO
ALTER DATABASE [VoteOnline] SET  READ_WRITE 
GO
