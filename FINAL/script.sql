USE [master]
GO
/****** Object:  Database [ay7agaaa]    Script Date: 19-May-20 2:06:21 AM ******/
CREATE DATABASE [ay7agaaa]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ay7agaaa', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ay7agaaa.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ay7agaaa_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ay7agaaa_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ay7agaaa] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ay7agaaa].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ay7agaaa] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ay7agaaa] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ay7agaaa] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ay7agaaa] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ay7agaaa] SET ARITHABORT OFF 
GO
ALTER DATABASE [ay7agaaa] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ay7agaaa] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ay7agaaa] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ay7agaaa] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ay7agaaa] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ay7agaaa] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ay7agaaa] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ay7agaaa] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ay7agaaa] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ay7agaaa] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ay7agaaa] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ay7agaaa] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ay7agaaa] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ay7agaaa] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ay7agaaa] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ay7agaaa] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ay7agaaa] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ay7agaaa] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ay7agaaa] SET  MULTI_USER 
GO
ALTER DATABASE [ay7agaaa] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ay7agaaa] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ay7agaaa] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ay7agaaa] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [ay7agaaa]
GO
/****** Object:  Table [dbo].[DeviceInfo]    Script Date: 19-May-20 2:06:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceInfo](
	[pinId] [int] NOT NULL,
	[controlledId] [int] NOT NULL,
	[deviceName] [nvarchar](20) NOT NULL,
	[deviceModel] [nvarchar](20) NOT NULL,
	[deviceSerial] [nvarchar](20) NOT NULL,
	[deviceState] [int] NOT NULL,
	[pinType] [nvarchar](20) NOT NULL,
	[deviceMovability] [int] NOT NULL,
	[deviceLastState] [nvarchar](10) NOT NULL,
	[deviceDegree] [int] NOT NULL,
	[roomId] [int] NOT NULL,
	[objectId] [int] NULL,
	[deviceUnit] [nchar](10) NOT NULL,
	[DeviceOff] [nvarchar](max) NULL,
	[DeviceOn] [nvarchar](max) NULL,
 CONSTRAINT [PK__DeviceIn__7FB89FD2CF983CD0] PRIMARY KEY CLUSTERED 
(
	[pinId] ASC,
	[controlledId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DeviceLog]    Script Date: 19-May-20 2:06:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceLog](
	[pinId] [int] NOT NULL,
	[controlledId] [int] NOT NULL,
	[deviceCurrentState] [nchar](10) NOT NULL,
	[deviceDegree] [int] NOT NULL,
	[primaryKey] [int] IDENTITY(0,1) NOT NULL,
	[currentDate] [datetime2](3) NULL,
PRIMARY KEY CLUSTERED 
(
	[primaryKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DeviceMode]    Script Date: 19-May-20 2:06:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceMode](
	[modeId] [int] NOT NULL,
	[pinId] [int] NOT NULL,
	[controlledId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Home]    Script Date: 19-May-20 2:06:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Home](
	[homaAddress] [nvarchar](100) NOT NULL,
	[homeDescription] [nvarchar](100) NULL,
	[homeId] [int] IDENTITY(1,1000) NOT NULL,
	[userId] [int] NOT NULL,
 CONSTRAINT [PK_Home] PRIMARY KEY CLUSTERED 
(
	[homeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ModeInfo]    Script Date: 19-May-20 2:06:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModeInfo](
	[modeName] [nchar](10) NOT NULL,
	[modeId] [int] NOT NULL,
	[modeDuration] [int] NULL,
 CONSTRAINT [PK_ModeInfo] PRIMARY KEY CLUSTERED 
(
	[modeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoomInfo]    Script Date: 19-May-20 2:06:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomInfo](
	[roomId] [int] NOT NULL,
	[roomTitle] [nvarchar](30) NOT NULL,
	[roomDescription] [nvarchar](100) NULL,
	[hoomId] [int] NOT NULL,
	[roomImage] [nvarchar](max) NULL,
 CONSTRAINT [PK__RoomInfo__6C3BF5BE525404D7] PRIMARY KEY CLUSTERED 
(
	[roomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoomMode]    Script Date: 19-May-20 2:06:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomMode](
	[roomId] [int] NOT NULL,
	[modeId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RootTable]    Script Date: 19-May-20 2:06:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RootTable](
	[objectId] [int] NOT NULL,
	[objectTitle] [nvarchar](20) NOT NULL,
	[objectDescription] [nvarchar](100) NULL,
	[objectRoot] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[objectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 19-May-20 2:06:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[userPassword] [nvarchar](50) NOT NULL,
	[userE_mail] [nvarchar](100) NOT NULL,
	[userName] [nvarchar](20) NOT NULL,
	[userPhoneNum] [int] NOT NULL,
	[userId] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[DeviceInfo]  WITH CHECK ADD  CONSTRAINT [fk_device_room] FOREIGN KEY([roomId])
REFERENCES [dbo].[RoomInfo] ([roomId])
GO
ALTER TABLE [dbo].[DeviceInfo] CHECK CONSTRAINT [fk_device_room]
GO
ALTER TABLE [dbo].[DeviceInfo]  WITH CHECK ADD  CONSTRAINT [FK_DeviceInfo_Root] FOREIGN KEY([objectId])
REFERENCES [dbo].[RootTable] ([objectId])
GO
ALTER TABLE [dbo].[DeviceInfo] CHECK CONSTRAINT [FK_DeviceInfo_Root]
GO
ALTER TABLE [dbo].[DeviceLog]  WITH CHECK ADD  CONSTRAINT [FK_DeviceLog_DeviceInfo] FOREIGN KEY([pinId], [controlledId])
REFERENCES [dbo].[DeviceInfo] ([pinId], [controlledId])
GO
ALTER TABLE [dbo].[DeviceLog] CHECK CONSTRAINT [FK_DeviceLog_DeviceInfo]
GO
ALTER TABLE [dbo].[DeviceMode]  WITH CHECK ADD  CONSTRAINT [FK_DeviceMode_ModeInfo] FOREIGN KEY([modeId])
REFERENCES [dbo].[ModeInfo] ([modeId])
GO
ALTER TABLE [dbo].[DeviceMode] CHECK CONSTRAINT [FK_DeviceMode_ModeInfo]
GO
ALTER TABLE [dbo].[DeviceMode]  WITH CHECK ADD  CONSTRAINT [FK_P_DeviceMode_DeviceInfo] FOREIGN KEY([pinId], [controlledId])
REFERENCES [dbo].[DeviceInfo] ([pinId], [controlledId])
GO
ALTER TABLE [dbo].[DeviceMode] CHECK CONSTRAINT [FK_P_DeviceMode_DeviceInfo]
GO
ALTER TABLE [dbo].[Home]  WITH CHECK ADD  CONSTRAINT [FK_Home_UserInfo] FOREIGN KEY([userId])
REFERENCES [dbo].[UserInfo] ([userId])
GO
ALTER TABLE [dbo].[Home] CHECK CONSTRAINT [FK_Home_UserInfo]
GO
ALTER TABLE [dbo].[RoomInfo]  WITH CHECK ADD  CONSTRAINT [FK_RoomInfo_Home] FOREIGN KEY([hoomId])
REFERENCES [dbo].[Home] ([homeId])
GO
ALTER TABLE [dbo].[RoomInfo] CHECK CONSTRAINT [FK_RoomInfo_Home]
GO
ALTER TABLE [dbo].[RoomMode]  WITH CHECK ADD  CONSTRAINT [FK_RoomMode_ModeInfo] FOREIGN KEY([modeId])
REFERENCES [dbo].[ModeInfo] ([modeId])
GO
ALTER TABLE [dbo].[RoomMode] CHECK CONSTRAINT [FK_RoomMode_ModeInfo]
GO
ALTER TABLE [dbo].[RoomMode]  WITH CHECK ADD  CONSTRAINT [FK_RoomMode_RoomInfo] FOREIGN KEY([roomId])
REFERENCES [dbo].[RoomInfo] ([roomId])
GO
ALTER TABLE [dbo].[RoomMode] CHECK CONSTRAINT [FK_RoomMode_RoomInfo]
GO
ALTER TABLE [dbo].[RootTable]  WITH CHECK ADD  CONSTRAINT [fk_root_id] FOREIGN KEY([objectRoot])
REFERENCES [dbo].[RootTable] ([objectId])
GO
ALTER TABLE [dbo].[RootTable] CHECK CONSTRAINT [fk_root_id]
GO
USE [master]
GO
ALTER DATABASE [ay7agaaa] SET  READ_WRITE 
GO
