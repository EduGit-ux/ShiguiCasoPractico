USE [master]
GO
/****** Object:  Database [BBDDCasoPractico]    Script Date: 29/5/2022 23:44:45 ******/
CREATE DATABASE [BBDDCasoPractico]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BBDDCasoPractico', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BBDDCasoPractico.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BBDDCasoPractico_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BBDDCasoPractico_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BBDDCasoPractico] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BBDDCasoPractico].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BBDDCasoPractico] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET ARITHABORT OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BBDDCasoPractico] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BBDDCasoPractico] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BBDDCasoPractico] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BBDDCasoPractico] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET RECOVERY FULL 
GO
ALTER DATABASE [BBDDCasoPractico] SET  MULTI_USER 
GO
ALTER DATABASE [BBDDCasoPractico] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BBDDCasoPractico] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BBDDCasoPractico] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BBDDCasoPractico] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BBDDCasoPractico] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BBDDCasoPractico] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BBDDCasoPractico', N'ON'
GO
ALTER DATABASE [BBDDCasoPractico] SET QUERY_STORE = OFF
GO
USE [BBDDCasoPractico]
GO
/****** Object:  Table [dbo].[cliente]    Script Date: 29/5/2022 23:44:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cliente](
	[cl_id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[cl_identificacion] [varchar](30) NOT NULL,
	[cl_contrasenia] [varchar](30) NOT NULL,
	[cl_estado] [bit] NOT NULL,
	[cl_nombre] [varchar](30) NOT NULL,
	[cl_genero] [varchar](30) NOT NULL,
	[cl_edad] [int] NOT NULL,
	[cl_direccion] [varchar](50) NOT NULL,
	[cl_telefono] [varchar](20) NOT NULL,
 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED 
(
	[cl_id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cuentas]    Script Date: 29/5/2022 23:44:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cuentas](
	[cu_numero_cuenta] [varchar](30) NOT NULL,
	[cu_id_cliente] [int] NOT NULL,
	[cu_saldo_inicial] [money] NOT NULL,
	[cu_tipo] [varchar](30) NOT NULL,
	[cu_estado] [bit] NOT NULL,
 CONSTRAINT [PK__cuentas__5138EEC71FAE4CF6] PRIMARY KEY CLUSTERED 
(
	[cu_numero_cuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[movimientos]    Script Date: 29/5/2022 23:44:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[movimientos](
	[mo_id_movimiento] [int] IDENTITY(1,1) NOT NULL,
	[mo_numero_cuenta] [varchar](30) NOT NULL,
	[mo_fecha] [datetime] NOT NULL,
	[mo_tipo_movimiento] [varchar](30) NOT NULL,
	[mo_saldo_inicial] [money] NOT NULL,
	[mo_movimientos] [money] NOT NULL,
	[mo_saldo_disponible] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[mo_id_movimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[cliente] ON 

INSERT [dbo].[cliente] ([cl_id_cliente], [cl_identificacion], [cl_contrasenia], [cl_estado], [cl_nombre], [cl_genero], [cl_edad], [cl_direccion], [cl_telefono]) VALUES (1, N'1712365462', N'1234', 1, N'Jose Lema', N'Masculino', 30, N'Otavalo sn y Principal', N'0985463251')
INSERT [dbo].[cliente] ([cl_id_cliente], [cl_identificacion], [cl_contrasenia], [cl_estado], [cl_nombre], [cl_genero], [cl_edad], [cl_direccion], [cl_telefono]) VALUES (2, N'0503685624', N'4578', 1, N'Marianela Montalvo', N'Femenino', 25, N'Amazonas y NNUU', N'0988765215')
INSERT [dbo].[cliente] ([cl_id_cliente], [cl_identificacion], [cl_contrasenia], [cl_estado], [cl_nombre], [cl_genero], [cl_edad], [cl_direccion], [cl_telefono]) VALUES (3, N'1785695265', N'5684', 1, N'Juan Osorio', N'Masculino', 65, N'13 Junio Equinoccial', N'0698563251')
INSERT [dbo].[cliente] ([cl_id_cliente], [cl_identificacion], [cl_contrasenia], [cl_estado], [cl_nombre], [cl_genero], [cl_edad], [cl_direccion], [cl_telefono]) VALUES (4, N'0503695842', N'1234', 1, N'Carlos Morales', N'Masculino', 52, N'Shyris SN y Japom', N'0965851562')
INSERT [dbo].[cliente] ([cl_id_cliente], [cl_identificacion], [cl_contrasenia], [cl_estado], [cl_nombre], [cl_genero], [cl_edad], [cl_direccion], [cl_telefono]) VALUES (5, N'1785696541', N'1234', 1, N'Luis Yepez', N'Masculino', 5, N'La Napo Trebol', N'0965851562')
SET IDENTITY_INSERT [dbo].[cliente] OFF
GO
INSERT [dbo].[cuentas] ([cu_numero_cuenta], [cu_id_cliente], [cu_saldo_inicial], [cu_tipo], [cu_estado]) VALUES (N'225487', 2, 100.0000, N'Corriente', 1)
INSERT [dbo].[cuentas] ([cu_numero_cuenta], [cu_id_cliente], [cu_saldo_inicial], [cu_tipo], [cu_estado]) VALUES (N'478758', 1, 2000.0000, N'Ahorro', 1)
INSERT [dbo].[cuentas] ([cu_numero_cuenta], [cu_id_cliente], [cu_saldo_inicial], [cu_tipo], [cu_estado]) VALUES (N'495878', 3, 0.0000, N'Ahorro', 1)
INSERT [dbo].[cuentas] ([cu_numero_cuenta], [cu_id_cliente], [cu_saldo_inicial], [cu_tipo], [cu_estado]) VALUES (N'496825', 2, 540.0000, N'Ahorro', 1)
INSERT [dbo].[cuentas] ([cu_numero_cuenta], [cu_id_cliente], [cu_saldo_inicial], [cu_tipo], [cu_estado]) VALUES (N'585545', 1, 1000.0000, N'Corriente', 1)
GO
SET IDENTITY_INSERT [dbo].[movimientos] ON 

INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_saldo_inicial], [mo_movimientos], [mo_saldo_disponible]) VALUES (1, N'225487', CAST(N'2022-05-29T16:47:00.043' AS DateTime), N'Credito', 10000.0000, 200.0000, 30000.0000)
INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_saldo_inicial], [mo_movimientos], [mo_saldo_disponible]) VALUES (2, N'478758', CAST(N'2022-05-29T18:53:04.850' AS DateTime), N'Debito', 2000.0000, 575.0000, 1425.0000)
INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_saldo_inicial], [mo_movimientos], [mo_saldo_disponible]) VALUES (6, N'495878', CAST(N'2022-05-29T19:30:19.560' AS DateTime), N'Credito', 0.0000, 150.0000, 150.0000)
INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_saldo_inicial], [mo_movimientos], [mo_saldo_disponible]) VALUES (7, N'495878', CAST(N'2022-05-29T19:31:55.283' AS DateTime), N'Credito', 0.0000, 150.0000, 300.0000)
INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_saldo_inicial], [mo_movimientos], [mo_saldo_disponible]) VALUES (39, N'496825', CAST(N'2022-05-29T22:51:42.403' AS DateTime), N'Credito', 540.0000, 545.0000, 1085.0000)
INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_saldo_inicial], [mo_movimientos], [mo_saldo_disponible]) VALUES (40, N'496825', CAST(N'2022-05-29T22:52:27.390' AS DateTime), N'Debito', 540.0000, 545.0000, 540.0000)
INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_saldo_inicial], [mo_movimientos], [mo_saldo_disponible]) VALUES (41, N'496825', CAST(N'2022-05-29T23:03:01.320' AS DateTime), N'Credito', 540.0000, 540.0000, 1080.0000)
INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_saldo_inicial], [mo_movimientos], [mo_saldo_disponible]) VALUES (42, N'496825', CAST(N'2022-05-29T23:03:58.103' AS DateTime), N'Credito', 540.0000, 540.0000, 1620.0000)
INSERT [dbo].[movimientos] ([mo_id_movimiento], [mo_numero_cuenta], [mo_fecha], [mo_tipo_movimiento], [mo_saldo_inicial], [mo_movimientos], [mo_saldo_disponible]) VALUES (43, N'496825', CAST(N'2022-05-29T23:05:40.493' AS DateTime), N'Debito', 540.0000, 5.0000, 1615.0000)
SET IDENTITY_INSERT [dbo].[movimientos] OFF
GO
ALTER TABLE [dbo].[cuentas]  WITH CHECK ADD  CONSTRAINT [FK_cuentas_cliente] FOREIGN KEY([cu_id_cliente])
REFERENCES [dbo].[cliente] ([cl_id_cliente])
GO
ALTER TABLE [dbo].[cuentas] CHECK CONSTRAINT [FK_cuentas_cliente]
GO
ALTER TABLE [dbo].[movimientos]  WITH CHECK ADD  CONSTRAINT [FK_movimientos_cuentas] FOREIGN KEY([mo_numero_cuenta])
REFERENCES [dbo].[cuentas] ([cu_numero_cuenta])
GO
ALTER TABLE [dbo].[movimientos] CHECK CONSTRAINT [FK_movimientos_cuentas]
GO
USE [master]
GO
ALTER DATABASE [BBDDCasoPractico] SET  READ_WRITE 
GO
