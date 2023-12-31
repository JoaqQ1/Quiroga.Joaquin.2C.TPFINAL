USE [master]
GO
/****** Object:  Database [Aerolinea]    Script Date: 21/11/2023 15:02:59 ******/
CREATE DATABASE [Aerolinea]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Aerolinea', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Aerolinea.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Aerolinea_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Aerolinea_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Aerolinea] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Aerolinea].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Aerolinea] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Aerolinea] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Aerolinea] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Aerolinea] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Aerolinea] SET ARITHABORT OFF 
GO
ALTER DATABASE [Aerolinea] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Aerolinea] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Aerolinea] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Aerolinea] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Aerolinea] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Aerolinea] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Aerolinea] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Aerolinea] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Aerolinea] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Aerolinea] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Aerolinea] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Aerolinea] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Aerolinea] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Aerolinea] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Aerolinea] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Aerolinea] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Aerolinea] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Aerolinea] SET RECOVERY FULL 
GO
ALTER DATABASE [Aerolinea] SET  MULTI_USER 
GO
ALTER DATABASE [Aerolinea] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Aerolinea] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Aerolinea] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Aerolinea] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Aerolinea] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Aerolinea] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Aerolinea', N'ON'
GO
ALTER DATABASE [Aerolinea] SET QUERY_STORE = ON
GO
ALTER DATABASE [Aerolinea] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Aerolinea]
GO
/****** Object:  Table [dbo].[Aviones]    Script Date: 21/11/2023 15:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aviones](
	[id_avion] [int] IDENTITY(1,1) NOT NULL,
	[origen] [varchar](20) NULL,
	[destino] [varchar](20) NULL,
	[horas_de_vuelo] [int] NOT NULL,
	[costo] [float] NOT NULL,
	[hora_de_salida] [datetime] NOT NULL,
	[disponible] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 21/11/2023 15:02:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](20) NOT NULL,
	[apellido] [varchar](20) NOT NULL,
	[genero] [varchar](20) NOT NULL,
	[correo] [varchar](30) NOT NULL,
	[password] [varchar](30) NOT NULL,
	[edad] [int] NOT NULL,
	[id_vuelo] [int] NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Aviones] ON 

INSERT [dbo].[Aviones] ([id_avion], [origen], [destino], [horas_de_vuelo], [costo], [hora_de_salida], [disponible]) VALUES (341, N'Argentina', N'España', 8, 4000, CAST(N'2023-11-21T03:34:20.973' AS DateTime), 1)
INSERT [dbo].[Aviones] ([id_avion], [origen], [destino], [horas_de_vuelo], [costo], [hora_de_salida], [disponible]) VALUES (342, N'Argentina', N'Venezuela', 1, 2232, CAST(N'2023-11-21T00:34:20.973' AS DateTime), 1)
INSERT [dbo].[Aviones] ([id_avion], [origen], [destino], [horas_de_vuelo], [costo], [hora_de_salida], [disponible]) VALUES (343, N'Argentina', N'Brasil', 1, 2364, CAST(N'2023-11-20T23:34:20.973' AS DateTime), 1)
INSERT [dbo].[Aviones] ([id_avion], [origen], [destino], [horas_de_vuelo], [costo], [hora_de_salida], [disponible]) VALUES (344, N'Argentina', N'Uruguay', 2, 2213, CAST(N'2023-11-20T23:34:20.973' AS DateTime), 1)
INSERT [dbo].[Aviones] ([id_avion], [origen], [destino], [horas_de_vuelo], [costo], [hora_de_salida], [disponible]) VALUES (345, N'Argentina', N'Mexico', 4, 3000, CAST(N'2023-11-21T01:34:20.973' AS DateTime), 1)
INSERT [dbo].[Aviones] ([id_avion], [origen], [destino], [horas_de_vuelo], [costo], [hora_de_salida], [disponible]) VALUES (346, N'Argentina', N'Venezuela', 2, 2239, CAST(N'2023-11-21T01:34:20.973' AS DateTime), 1)
INSERT [dbo].[Aviones] ([id_avion], [origen], [destino], [horas_de_vuelo], [costo], [hora_de_salida], [disponible]) VALUES (347, N'Argentina', N'Paraguay', 2, 1800, CAST(N'2023-11-21T03:34:20.973' AS DateTime), 1)
INSERT [dbo].[Aviones] ([id_avion], [origen], [destino], [horas_de_vuelo], [costo], [hora_de_salida], [disponible]) VALUES (348, N'Argentina', N'España', 8, 4000, CAST(N'2023-11-20T23:34:20.973' AS DateTime), 1)
INSERT [dbo].[Aviones] ([id_avion], [origen], [destino], [horas_de_vuelo], [costo], [hora_de_salida], [disponible]) VALUES (349, N'Argentina', N'Peru', 1, 1759, CAST(N'2023-11-21T03:34:20.973' AS DateTime), 1)
INSERT [dbo].[Aviones] ([id_avion], [origen], [destino], [horas_de_vuelo], [costo], [hora_de_salida], [disponible]) VALUES (350, N'Argentina', N'Mexico', 4, 3000, CAST(N'2023-11-21T01:34:20.973' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Aviones] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([id_usuario], [nombre], [apellido], [genero], [correo], [password], [edad], [id_vuelo]) VALUES (11, N'Joaco', N'Quirgoa', N'Otro', N'Joaco', N'Apolo', 32, 335)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
USE [master]
GO
ALTER DATABASE [Aerolinea] SET  READ_WRITE 
GO
