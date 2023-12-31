USE [master]
GO
/****** Object:  Database [VoteOnline]    Script Date: 2023/10/4 上午 08:01:20 ******/
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
/****** Object:  Table [dbo].[VoteItems]    Script Date: 2023/10/4 上午 08:01:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoteItems](
	[VoteItemId] [nvarchar](50) NOT NULL,
	[ItemName] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK__VoteItem__04803B287FC4EE91] PRIMARY KEY CLUSTERED 
(
	[VoteItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VoteRecords]    Script Date: 2023/10/4 上午 08:01:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoteRecords](
	[VoteId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](10) NOT NULL,
	[VoteItemId] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__VoteReco__52F015C287364848] PRIMARY KEY CLUSTERED 
(
	[VoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VoteCountsView]    Script Date: 2023/10/4 上午 08:01:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VoteCountsView] AS
SELECT dbo.VoteItems.VoteItemId, dbo.VoteItems.ItemName, COUNT(dbo.VoteRecords.VoteItemId) AS VoteCount
FROM dbo.VoteItems
LEFT JOIN dbo.VoteRecords ON dbo.VoteItems.VoteItemId = dbo.VoteRecords.VoteItemId
GROUP BY dbo.VoteItems.VoteItemId, dbo.VoteItems.ItemName
GO
INSERT [dbo].[VoteItems] ([VoteItemId], [ItemName]) VALUES (N'1', N'耳機')
INSERT [dbo].[VoteItems] ([VoteItemId], [ItemName]) VALUES (N'2', N'電腦')
INSERT [dbo].[VoteItems] ([VoteItemId], [ItemName]) VALUES (N'3', N'滑鼠')
GO
SET IDENTITY_INSERT [dbo].[VoteRecords] ON 

INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (1, N'Leo', N'1')
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (2, N'Sandy', N'1')
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (3, N'Sandy', N'2')
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (4, N'Randy', N'2')
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (5, N'RSY', N'2')
INSERT [dbo].[VoteRecords] ([VoteId], [UserName], [VoteItemId]) VALUES (6, N'jerry', N'1')
SET IDENTITY_INSERT [dbo].[VoteRecords] OFF
GO
ALTER TABLE [dbo].[VoteRecords]  WITH CHECK ADD  CONSTRAINT [FK__VoteRecor__VoteI__267ABA7A] FOREIGN KEY([VoteItemId])
REFERENCES [dbo].[VoteItems] ([VoteItemId])
GO
ALTER TABLE [dbo].[VoteRecords] CHECK CONSTRAINT [FK__VoteRecor__VoteI__267ABA7A]
GO
/****** Object:  StoredProcedure [dbo].[GetVoteCounts]    Script Date: 2023/10/4 上午 08:01:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetVoteCounts]
AS
BEGIN
    SELECT dbo.VoteItems.VoteItemId, dbo.VoteItems.ItemName, COUNT(dbo.VoteRecords.VoteItemId) AS VoteCount
    FROM dbo.VoteItems
    LEFT JOIN dbo.VoteRecords ON dbo.VoteItems.VoteItemId = dbo.VoteRecords.VoteItemId
    GROUP BY dbo.VoteItems.VoteItemId, dbo.VoteItems.ItemName
END

exec dbo.GetVoteCounts
GO
USE [master]
GO
ALTER DATABASE [VoteOnline] SET  READ_WRITE 
GO
