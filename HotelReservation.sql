USE [master]
GO
/****** Object:  Database [hotel_reservation]    Script Date: 24/11/2024 5:37:28 p. m. ******/
CREATE DATABASE [hotel_reservation]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'hotel_reservation', FILENAME = N'C:\SQLData\hotel_reservation.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'hotel_reservation_log', FILENAME = N'C:\SQLData\logs\hotel_reservation_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [hotel_reservation] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [hotel_reservation].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [hotel_reservation] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [hotel_reservation] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [hotel_reservation] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [hotel_reservation] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [hotel_reservation] SET ARITHABORT OFF 
GO
ALTER DATABASE [hotel_reservation] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [hotel_reservation] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [hotel_reservation] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [hotel_reservation] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [hotel_reservation] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [hotel_reservation] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [hotel_reservation] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [hotel_reservation] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [hotel_reservation] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [hotel_reservation] SET  ENABLE_BROKER 
GO
ALTER DATABASE [hotel_reservation] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [hotel_reservation] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [hotel_reservation] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [hotel_reservation] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [hotel_reservation] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [hotel_reservation] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [hotel_reservation] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [hotel_reservation] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [hotel_reservation] SET  MULTI_USER 
GO
ALTER DATABASE [hotel_reservation] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [hotel_reservation] SET DB_CHAINING OFF 
GO
ALTER DATABASE [hotel_reservation] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [hotel_reservation] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [hotel_reservation] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [hotel_reservation] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [hotel_reservation] SET QUERY_STORE = ON
GO
ALTER DATABASE [hotel_reservation] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [hotel_reservation]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 24/11/2024 5:37:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 24/11/2024 5:37:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[BookingId] [int] IDENTITY(1,1) NOT NULL,
	[ReservationDate] [datetime2](7) NOT NULL,
	[PeopleNumber] [int] NOT NULL,
	[Estado] [nvarchar](max) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
 CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED 
(
	[BookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 24/11/2024 5:37:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailsReservations]    Script Date: 24/11/2024 5:37:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailsReservations](
	[DetailReservationId] [int] IDENTITY(1,1) NOT NULL,
	[BookingId] [int] NOT NULL,
 CONSTRAINT [PK_DetailsReservations] PRIMARY KEY CLUSTERED 
(
	[DetailReservationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 24/11/2024 5:37:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[ServiceId] [int] IDENTITY(1,1) NOT NULL,
	[ServiceName] [nvarchar](max) NOT NULL,
	[ServiceDescription] [nvarchar](max) NOT NULL,
	[Price] [int] NOT NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 24/11/2024 5:37:29 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241122180151_FirstMigration', N'6.0.3')
GO
SET IDENTITY_INSERT [dbo].[Bookings] ON 
GO
INSERT [dbo].[Bookings] ([BookingId], [ReservationDate], [PeopleNumber], [Estado], [CustomerId], [ServiceId]) VALUES (1, CAST(N'2024-11-25T00:00:00.0000000' AS DateTime2), 2, N'Pendiente', 1, 1)
GO
INSERT [dbo].[Bookings] ([BookingId], [ReservationDate], [PeopleNumber], [Estado], [CustomerId], [ServiceId]) VALUES (2, CAST(N'2024-11-30T00:00:00.0000000' AS DateTime2), 3, N'Preuba', 4, 1)
GO
INSERT [dbo].[Bookings] ([BookingId], [ReservationDate], [PeopleNumber], [Estado], [CustomerId], [ServiceId]) VALUES (3, CAST(N'2024-11-23T00:00:00.0000000' AS DateTime2), 1, N'activo', 1, 1)
GO
INSERT [dbo].[Bookings] ([BookingId], [ReservationDate], [PeopleNumber], [Estado], [CustomerId], [ServiceId]) VALUES (4, CAST(N'2024-11-30T00:00:00.0000000' AS DateTime2), 5, N'activa', 1, 1)
GO
INSERT [dbo].[Bookings] ([BookingId], [ReservationDate], [PeopleNumber], [Estado], [CustomerId], [ServiceId]) VALUES (5, CAST(N'2024-11-28T00:00:00.0000000' AS DateTime2), 1, N'activo', 4, 1)
GO
INSERT [dbo].[Bookings] ([BookingId], [ReservationDate], [PeopleNumber], [Estado], [CustomerId], [ServiceId]) VALUES (8, CAST(N'2024-11-26T09:23:00.0000000' AS DateTime2), 1, N'activo', 1, 1)
GO
INSERT [dbo].[Bookings] ([BookingId], [ReservationDate], [PeopleNumber], [Estado], [CustomerId], [ServiceId]) VALUES (9, CAST(N'2024-11-26T12:23:00.0000000' AS DateTime2), 1, N'activo', 5, 2)
GO
SET IDENTITY_INSERT [dbo].[Bookings] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 
GO
INSERT [dbo].[Customers] ([CustomerId], [Name], [Email]) VALUES (1, N'PRUEBA', N'PRUEBA')
GO
INSERT [dbo].[Customers] ([CustomerId], [Name], [Email]) VALUES (3, N'string', N'string')
GO
INSERT [dbo].[Customers] ([CustomerId], [Name], [Email]) VALUES (4, N'nuevo', N'llaraveje@gmail.com')
GO
INSERT [dbo].[Customers] ([CustomerId], [Name], [Email]) VALUES (5, N'jllarave', N'llaraveprueba@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Services] ON 
GO
INSERT [dbo].[Services] ([ServiceId], [ServiceName], [ServiceDescription], [Price]) VALUES (1, N'prueba', N'prueba', 20)
GO
INSERT [dbo].[Services] ([ServiceId], [ServiceName], [ServiceDescription], [Price]) VALUES (2, N'Servicio', N'que buen servicio', 200)
GO
SET IDENTITY_INSERT [dbo].[Services] OFF
GO
/****** Object:  Index [IX_Bookings_CustomerId]    Script Date: 24/11/2024 5:37:29 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Bookings_CustomerId] ON [dbo].[Bookings]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bookings_ServiceId]    Script Date: 24/11/2024 5:37:29 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Bookings_ServiceId] ON [dbo].[Bookings]
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DetailsReservations_BookingId]    Script Date: 24/11/2024 5:37:29 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_DetailsReservations_BookingId] ON [dbo].[DetailsReservations]
(
	[BookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_Customers_CustomerId]
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_Services_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([ServiceId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_Services_ServiceId]
GO
ALTER TABLE [dbo].[DetailsReservations]  WITH CHECK ADD  CONSTRAINT [FK_DetailsReservations_Bookings_BookingId] FOREIGN KEY([BookingId])
REFERENCES [dbo].[Bookings] ([BookingId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetailsReservations] CHECK CONSTRAINT [FK_DetailsReservations_Bookings_BookingId]
GO
USE [master]
GO
ALTER DATABASE [hotel_reservation] SET  READ_WRITE 
GO
