USE [master]
GO
/****** Object:  Database [PMS]    Script Date: 11/13/2022 12:34:39 PM ******/
CREATE DATABASE [PMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PMS', FILENAME = N'C:\Users\ranga.a\PMS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PMS_log', FILENAME = N'C:\Users\ranga.a\PMS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PMS] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [PMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PMS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PMS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PMS] SET  MULTI_USER 
GO
ALTER DATABASE [PMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PMS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PMS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PMS] SET QUERY_STORE = OFF
GO
USE [PMS]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [PMS]
GO
/****** Object:  Table [dbo].[AccessGroup]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessGroup](
	[AccessGroupId] [int] IDENTITY(1,1) NOT NULL,
	[AccessGroupName] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_AccessGroup] PRIMARY KEY CLUSTERED 
(
	[AccessGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccessGroupClaims]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessGroupClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccessGroupId] [int] NOT NULL,
	[ClaimId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_AccessGroupClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[AppointmentId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](256) NOT NULL,
	[AppointmentTypeId] [int] NOT NULL,
	[DesignationId] [int] NOT NULL,
	[AppointmentFrom] [date] NULL,
	[AppointmentTo] [date] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[Comment] [nvarchar](256) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppointmentType]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppointmentType](
	[AppointmentTypeId] [int] IDENTITY(1,1) NOT NULL,
	[AppointmentTypeName] [nvarchar](200) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_AppointmentType] PRIMARY KEY CLUSTERED 
(
	[AppointmentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](150) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[AccessGroupId] [int] NOT NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](256) NOT NULL,
	[AccessGroupClaimId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](256) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](150) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](256) NOT NULL,
	[EmployeeNumber] [nvarchar](50) NULL,
	[EmployeeTitle] [int] NOT NULL,
	[FirstName] [nvarchar](150) NOT NULL,
	[LastName] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[UserName] [nvarchar](150) NOT NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[FacultyId] [int] NULL,
	[DepartmentId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](150) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsAcademicUser] [bit] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[AccessFailedCount] [int] NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[EmailConfirmed] [bit] NULL,
	[LockoutEnabled] [bit] NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CalendarPeriod]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CalendarPeriod](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PeriodName] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_CalendarPeriod] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Campus]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Campus](
	[CampusId] [int] IDENTITY(1,1) NOT NULL,
	[CampusCode] [nvarchar](100) NOT NULL,
	[CampusName] [nvarchar](200) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Campus] PRIMARY KEY CLUSTERED 
(
	[CampusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Claim]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Claim](
	[ClaimId] [int] IDENTITY(1,1) NOT NULL,
	[ClaimName] [nvarchar](256) NOT NULL,
	[ClaimValue] [nvarchar](500) NOT NULL,
	[SubOperation] [nvarchar](100) NULL,
	[Description] [nvarchar](256) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Claim] PRIMARY KEY CLUSTERED 
(
	[ClaimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConductedLectures]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConductedLectures](
	[CLId] [int] IDENTITY(1,1) NOT NULL,
	[TimetableId] [int] NOT NULL,
	[ActualLectureDate] [date] NOT NULL,
	[ActualFromTime] [time](7) NOT NULL,
	[ActualToTime] [time](7) NOT NULL,
	[ActualLocationId] [int] NULL,
	[CampusId] [int] NULL,
	[StudentBatches] [nvarchar](500) NULL,
	[StudentCount] [int] NULL,
	[StudentAttendanceSheetLocation] [nvarchar](500) NULL,
	[Comment] [nvarchar](500) NULL,
	[CurrentStage] [int] NULL,
	[CurrentStageDisplayName] [nvarchar](256) NULL,
	[IsApprovedOrRejected] [bit] NULL,
	[ApprovedOrRejectedDate] [datetime] NULL,
	[ApprovedOrRejectedBy] [nvarchar](256) NULL,
	[ApprovedOrRejectedRemark] [nvarchar](500) NULL,
	[IsOpenForModerations] [bit] NOT NULL,
	[ModerationOpenedDate] [datetime] NULL,
	[ModerationOpenedBy] [nvarchar](256) NULL,
	[ModerationOpenedRemark] [nvarchar](500) NULL,
	[PaymentAmount] [float] NULL,
	[IsFinalApproved] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ConductedLectures] PRIMARY KEY CLUSTERED 
(
	[CLId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConductedLecturesLog]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConductedLecturesLog](
	[CLLogId] [int] IDENTITY(1,1) NOT NULL,
	[CLId] [int] NOT NULL,
	[TimetableId] [int] NOT NULL,
	[ActualLectureDate] [date] NOT NULL,
	[ActualFromTime] [time](7) NOT NULL,
	[ActualToTime] [time](7) NOT NULL,
	[ActualLocationId] [int] NULL,
	[CampusId] [int] NULL,
	[StudentBatches] [nvarchar](500) NULL,
	[StudentCount] [int] NULL,
	[StudentAttendanceSheetLocation] [nvarchar](500) NULL,
	[Comment] [nvarchar](500) NULL,
	[CurrentStage] [int] NULL,
	[CurrentStageDisplayName] [nvarchar](256) NULL,
	[IsApprovedOrRejected] [bit] NULL,
	[ApprovedOrRejectedDate] [datetime] NULL,
	[ApprovedOrRejectedBy] [nvarchar](256) NULL,
	[ApprovedOrRejectedRemark] [nvarchar](500) NULL,
	[IsOpenForModerations] [bit] NOT NULL,
	[ModerationOpenedDate] [datetime] NULL,
	[ModerationOpenedBy] [nvarchar](256) NULL,
	[ModerationOpenedRemark] [nvarchar](500) NULL,
	[PaymentAmount] [float] NULL,
	[IsFinalApproved] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ConductedLecturesLog] PRIMARY KEY CLUSTERED 
(
	[CLLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConfigurationalSettings]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConfigurationalSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConfigurationKey] [nvarchar](250) NOT NULL,
	[IsFacultyWise] [bit] NOT NULL,
	[FacultyId] [int] NULL,
	[ConfigurationValue] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Configuration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Degree]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Degree](
	[DegreeId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Description] [nvarchar](2000) NULL,
	[FacultyId] [int] NULL,
	[InstituteId] [int] NULL,
	[DepartmentId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Degree] PRIMARY KEY CLUSTERED 
(
	[DegreeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentCode] [nvarchar](100) NULL,
	[DepartmentName] [nvarchar](500) NOT NULL,
	[HOD] [nvarchar](256) NULL,
	[FacultyId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Designation]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Designation](
	[DesignationId] [int] IDENTITY(1,1) NOT NULL,
	[DesignationName] [nvarchar](150) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Designation] PRIMARY KEY CLUSTERED 
(
	[DesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Faculty]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faculty](
	[FacultyId] [int] IDENTITY(1,1) NOT NULL,
	[FacultyCode] [nvarchar](100) NOT NULL,
	[FacultyName] [nvarchar](200) NOT NULL,
	[FacultyDean] [nvarchar](256) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Faculty] PRIMARY KEY CLUSTERED 
(
	[FacultyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Institute]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Institute](
	[InstituteId] [int] IDENTITY(1,1) NOT NULL,
	[InstituteCode] [nvarchar](100) NOT NULL,
	[InstituteName] [nvarchar](200) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Institute] PRIMARY KEY CLUSTERED 
(
	[InstituteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Intake]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Intake](
	[IntakeId] [int] IDENTITY(1,1) NOT NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
	[IntakeCode] [nvarchar](150) NULL,
	[IntakeName] [nvarchar](200) NULL,
	[IntakeYear] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Intake] PRIMARY KEY CLUSTERED 
(
	[IntakeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LectureHall]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LectureHall](
	[HallId] [int] IDENTITY(1,1) NOT NULL,
	[CampusId] [int] NOT NULL,
	[Building] [nvarchar](150) NULL,
	[Floor] [nvarchar](150) NULL,
	[HallName] [nvarchar](150) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_LectureHall] PRIMARY KEY CLUSTERED 
(
	[HallId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LecturerAssignments]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LecturerAssignments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SemesterId] [int] NOT NULL,
	[LecturerId] [nvarchar](256) NOT NULL,
	[SemesterSubjectId] [int] NOT NULL,
	[StudentBatches] [nvarchar](256) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_LecturerAssignments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LectureTimetable]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LectureTimetable](
	[TimetableId] [int] IDENTITY(1,1) NOT NULL,
	[SemesterId] [int] NOT NULL,
	[SemesterSubjectId] [int] NOT NULL,
	[LectureDate] [date] NULL,
	[FromTime] [time](7) NULL,
	[ToTime] [time](7) NULL,
	[LocationId] [int] NULL,
	[LectureTypeId] [int] NOT NULL,
	[LecturerId] [nvarchar](256) NULL,
	[StudentBatches] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_LectureTimetable] PRIMARY KEY CLUSTERED 
(
	[TimetableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LectureTimetableLog]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LectureTimetableLog](
	[TimetableLogId] [int] IDENTITY(1,1) NOT NULL,
	[TimetableId] [int] NOT NULL,
	[SemesterId] [int] NOT NULL,
	[SemesterSubjectId] [int] NOT NULL,
	[LectureDate] [date] NULL,
	[FromTime] [time](7) NULL,
	[ToTime] [time](7) NULL,
	[LocationId] [int] NULL,
	[LectureTypeId] [int] NOT NULL,
	[LecturerId] [nvarchar](256) NULL,
	[StudentBatches] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_LectureTimetableLog] PRIMARY KEY CLUSTERED 
(
	[TimetableLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LectureType]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LectureType](
	[LectureTypeId] [int] IDENTITY(1,1) NOT NULL,
	[LectureTypeName] [nvarchar](100) NOT NULL,
	[ConsiderMinimumStudentCount] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_LectureType] PRIMARY KEY CLUSTERED 
(
	[LectureTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[NotificationId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](256) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentRate]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentRate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DegreeId] [int] NULL,
	[SpecializationId] [int] NULL,
	[FacultyId] [int] NULL,
	[SubjectId] [int] NULL,
	[DesignationId] [int] NOT NULL,
	[LectureTypeId] [int] NULL,
	[RatePerHour] [float] NOT NULL,
	[OldRatePerHour] [float] NOT NULL,
	[SentForApproval] [bit] NULL,
	[SentToApprovalBy] [nvarchar](256) NULL,
	[SentToApprovalDate] [datetime] NULL,
	[IsApproved] [bit] NULL,
	[ApprovalOrRejectionRemark] [nvarchar](500) NULL,
	[ApprovedOrRejectedBy] [nvarchar](256) NULL,
	[ApprovedOrRejectedDate] [datetime] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_PaymentRate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentRateLog]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentRateLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DegreeId] [int] NULL,
	[SpecializationId] [int] NULL,
	[FacultyId] [int] NULL,
	[SubjectId] [int] NULL,
	[DesignationId] [int] NOT NULL,
	[LectureTypeId] [int] NULL,
	[RatePerHour] [float] NOT NULL,
	[OldRatePerHour] [float] NOT NULL,
	[SentForApproval] [bit] NULL,
	[SentToApprovalBy] [nvarchar](256) NULL,
	[SentToApprovalDate] [datetime] NULL,
	[IsApproved] [bit] NULL,
	[ApprovalOrRejectionRemark] [nvarchar](500) NULL,
	[ApprovedOrRejectedBy] [nvarchar](256) NULL,
	[ApprovedOrRejectedDate] [datetime] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[PaymentRateId] [int] NOT NULL,
 CONSTRAINT [PK_PaymentRateLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SemesterRegistration]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SemesterRegistration](
	[SemesterId] [int] IDENTITY(1,1) NOT NULL,
	[CalendarYear] [int] NULL,
	[CalendarPeriodId] [int] NULL,
	[IntakeYear] [int] NULL,
	[IntakeId] [int] NULL,
	[AcademicYear] [int] NULL,
	[AcademicSemester] [int] NULL,
	[FacultyId] [int] NULL,
	[InstituteId] [int] NULL,
	[DegreeId] [int] NULL,
	[SpecializationId] [int] NULL,
	[DepartmentId] [int] NULL,
	[FromDate] [date] NULL,
	[ToDate] [date] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SemesterRegistration] PRIMARY KEY CLUSTERED 
(
	[SemesterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SemesterSubject]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SemesterSubject](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SemesterRegistrationId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SemesterSubject_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SemesterSubjectLIC]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SemesterSubjectLIC](
	[SSLICId] [int] IDENTITY(1,1) NOT NULL,
	[SemesterSubjectId] [int] NOT NULL,
	[LICId] [nvarchar](256) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SemesterSubjectLIC] PRIMARY KEY CLUSTERED 
(
	[SSLICId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialization]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialization](
	[SpecializationId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](100) NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[DegreeId] [int] NULL,
	[InstituteId] [int] NULL,
	[DepartmentId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Specialization] PRIMARY KEY CLUSTERED 
(
	[SpecializationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentBatch]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentBatch](
	[StudentBatchId] [int] IDENTITY(1,1) NOT NULL,
	[SemesterRegistrationId] [int] NOT NULL,
	[BatchName] [nvarchar](100) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_StudentBatch] PRIMARY KEY CLUSTERED 
(
	[StudentBatchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectId] [int] IDENTITY(1,1) NOT NULL,
	[SubjectCode] [nvarchar](100) NOT NULL,
	[SubjectName] [nvarchar](256) NOT NULL,
	[IsCommon] [bit] NOT NULL,
	[DegreeId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubWorkflows]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubWorkflows](
	[SubWorkflowId] [int] IDENTITY(1,1) NOT NULL,
	[WorkflowId] [int] NOT NULL,
	[WorkflowRole] [nvarchar](128) NOT NULL,
	[WorkflowStep] [int] NOT NULL,
	[ConsideringArea] [nvarchar](150) NULL,
	[IsSpecificUser] [bit] NOT NULL,
	[WorkflowUser] [nvarchar](256) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SubWorkflows] PRIMARY KEY CLUSTERED 
(
	[SubWorkflowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Title]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Title](
	[TitleId] [int] IDENTITY(1,1) NOT NULL,
	[TitleName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Title] PRIMARY KEY CLUSTERED 
(
	[TitleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Workflows]    Script Date: 11/13/2022 12:34:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Workflows](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WorkflowName] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](256) NULL,
	[FacultyId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Workflow] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF_AspNetUsers_IsAcademicUser]  DEFAULT ((0)) FOR [IsAcademicUser]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  CONSTRAINT [DF_AspNetUsers_IsAdmin]  DEFAULT ((0)) FOR [IsAdmin]
GO
ALTER TABLE [dbo].[AccessGroupClaims]  WITH CHECK ADD  CONSTRAINT [FK_AccessGroupClaims_AccessGroup] FOREIGN KEY([AccessGroupId])
REFERENCES [dbo].[AccessGroup] ([AccessGroupId])
GO
ALTER TABLE [dbo].[AccessGroupClaims] CHECK CONSTRAINT [FK_AccessGroupClaims_AccessGroup]
GO
ALTER TABLE [dbo].[AccessGroupClaims]  WITH CHECK ADD  CONSTRAINT [FK_AccessGroupClaims_Claim] FOREIGN KEY([ClaimId])
REFERENCES [dbo].[Claim] ([ClaimId])
GO
ALTER TABLE [dbo].[AccessGroupClaims] CHECK CONSTRAINT [FK_AccessGroupClaims_Claim]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_AppointmentType] FOREIGN KEY([AppointmentTypeId])
REFERENCES [dbo].[AppointmentType] ([AppointmentTypeId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_AppointmentType]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_AspNetUsers]
GO
ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Designation] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[Designation] ([DesignationId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Designation]
GO
ALTER TABLE [dbo].[AspNetRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoles_AccessGroup] FOREIGN KEY([AccessGroupId])
REFERENCES [dbo].[AccessGroup] ([AccessGroupId])
GO
ALTER TABLE [dbo].[AspNetRoles] CHECK CONSTRAINT [FK_AspNetRoles_AccessGroup]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AccessGroupClaims] FOREIGN KEY([AccessGroupClaimId])
REFERENCES [dbo].[AccessGroupClaims] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AccessGroupClaims]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_Faculty] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculty] ([FacultyId])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_Faculty]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_Title] FOREIGN KEY([EmployeeTitle])
REFERENCES [dbo].[Title] ([TitleId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_Title]
GO
ALTER TABLE [dbo].[ConductedLectures]  WITH CHECK ADD  CONSTRAINT [FK_ConductedLectures_Campus] FOREIGN KEY([CampusId])
REFERENCES [dbo].[Campus] ([CampusId])
GO
ALTER TABLE [dbo].[ConductedLectures] CHECK CONSTRAINT [FK_ConductedLectures_Campus]
GO
ALTER TABLE [dbo].[ConductedLectures]  WITH CHECK ADD  CONSTRAINT [FK_ConductedLectures_LectureHall] FOREIGN KEY([ActualLocationId])
REFERENCES [dbo].[LectureHall] ([HallId])
GO
ALTER TABLE [dbo].[ConductedLectures] CHECK CONSTRAINT [FK_ConductedLectures_LectureHall]
GO
ALTER TABLE [dbo].[ConductedLectures]  WITH CHECK ADD  CONSTRAINT [FK_ConductedLectures_LectureTimetable] FOREIGN KEY([TimetableId])
REFERENCES [dbo].[LectureTimetable] ([TimetableId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConductedLectures] CHECK CONSTRAINT [FK_ConductedLectures_LectureTimetable]
GO
ALTER TABLE [dbo].[ConductedLectures]  WITH CHECK ADD  CONSTRAINT [FK_ConductedLectures_SubWorkflows] FOREIGN KEY([CurrentStage])
REFERENCES [dbo].[SubWorkflows] ([SubWorkflowId])
GO
ALTER TABLE [dbo].[ConductedLectures] CHECK CONSTRAINT [FK_ConductedLectures_SubWorkflows]
GO
ALTER TABLE [dbo].[ConductedLecturesLog]  WITH CHECK ADD  CONSTRAINT [FK_ConductedLecturesLog_Campus] FOREIGN KEY([CampusId])
REFERENCES [dbo].[Campus] ([CampusId])
GO
ALTER TABLE [dbo].[ConductedLecturesLog] CHECK CONSTRAINT [FK_ConductedLecturesLog_Campus]
GO
ALTER TABLE [dbo].[ConductedLecturesLog]  WITH CHECK ADD  CONSTRAINT [FK_ConductedLecturesLog_ConductedLectures] FOREIGN KEY([CLId])
REFERENCES [dbo].[ConductedLectures] ([CLId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConductedLecturesLog] CHECK CONSTRAINT [FK_ConductedLecturesLog_ConductedLectures]
GO
ALTER TABLE [dbo].[ConductedLecturesLog]  WITH CHECK ADD  CONSTRAINT [FK_ConductedLecturesLog_LectureHall] FOREIGN KEY([ActualLocationId])
REFERENCES [dbo].[LectureHall] ([HallId])
GO
ALTER TABLE [dbo].[ConductedLecturesLog] CHECK CONSTRAINT [FK_ConductedLecturesLog_LectureHall]
GO
ALTER TABLE [dbo].[ConductedLecturesLog]  WITH CHECK ADD  CONSTRAINT [FK_ConductedLecturesLog_LectureTimetable] FOREIGN KEY([TimetableId])
REFERENCES [dbo].[LectureTimetable] ([TimetableId])
GO
ALTER TABLE [dbo].[ConductedLecturesLog] CHECK CONSTRAINT [FK_ConductedLecturesLog_LectureTimetable]
GO
ALTER TABLE [dbo].[ConductedLecturesLog]  WITH CHECK ADD  CONSTRAINT [FK_ConductedLecturesLog_SubWorkflows] FOREIGN KEY([CurrentStage])
REFERENCES [dbo].[SubWorkflows] ([SubWorkflowId])
GO
ALTER TABLE [dbo].[ConductedLecturesLog] CHECK CONSTRAINT [FK_ConductedLecturesLog_SubWorkflows]
GO
ALTER TABLE [dbo].[ConfigurationalSettings]  WITH CHECK ADD  CONSTRAINT [FK_ConfigurationalSettings_Faculty] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculty] ([FacultyId])
GO
ALTER TABLE [dbo].[ConfigurationalSettings] CHECK CONSTRAINT [FK_ConfigurationalSettings_Faculty]
GO
ALTER TABLE [dbo].[Degree]  WITH CHECK ADD  CONSTRAINT [FK_Degree_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[Degree] CHECK CONSTRAINT [FK_Degree_Department]
GO
ALTER TABLE [dbo].[Degree]  WITH CHECK ADD  CONSTRAINT [FK_Degree_Faculty] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculty] ([FacultyId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Degree] CHECK CONSTRAINT [FK_Degree_Faculty]
GO
ALTER TABLE [dbo].[Degree]  WITH CHECK ADD  CONSTRAINT [FK_Degree_Institute] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institute] ([InstituteId])
GO
ALTER TABLE [dbo].[Degree] CHECK CONSTRAINT [FK_Degree_Institute]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_AspNetUsers] FOREIGN KEY([HOD])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_AspNetUsers]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Faculty] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculty] ([FacultyId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Faculty]
GO
ALTER TABLE [dbo].[Faculty]  WITH CHECK ADD  CONSTRAINT [FK_Faculty_AspNetUsers] FOREIGN KEY([FacultyDean])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Faculty] CHECK CONSTRAINT [FK_Faculty_AspNetUsers]
GO
ALTER TABLE [dbo].[LectureHall]  WITH CHECK ADD  CONSTRAINT [FK_LectureHall_Campus] FOREIGN KEY([CampusId])
REFERENCES [dbo].[Campus] ([CampusId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LectureHall] CHECK CONSTRAINT [FK_LectureHall_Campus]
GO
ALTER TABLE [dbo].[LecturerAssignments]  WITH CHECK ADD  CONSTRAINT [FK_LecturerAssignments_AspNetUsers] FOREIGN KEY([LecturerId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[LecturerAssignments] CHECK CONSTRAINT [FK_LecturerAssignments_AspNetUsers]
GO
ALTER TABLE [dbo].[LecturerAssignments]  WITH CHECK ADD  CONSTRAINT [FK_LecturerAssignments_SemesterRegistration] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[SemesterRegistration] ([SemesterId])
GO
ALTER TABLE [dbo].[LecturerAssignments] CHECK CONSTRAINT [FK_LecturerAssignments_SemesterRegistration]
GO
ALTER TABLE [dbo].[LecturerAssignments]  WITH CHECK ADD  CONSTRAINT [FK_LecturerAssignments_SemesterSubject] FOREIGN KEY([SemesterSubjectId])
REFERENCES [dbo].[SemesterSubject] ([Id])
GO
ALTER TABLE [dbo].[LecturerAssignments] CHECK CONSTRAINT [FK_LecturerAssignments_SemesterSubject]
GO
ALTER TABLE [dbo].[LectureTimetable]  WITH CHECK ADD  CONSTRAINT [FK_LectureTimetable_AspNetUsers] FOREIGN KEY([LecturerId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[LectureTimetable] CHECK CONSTRAINT [FK_LectureTimetable_AspNetUsers]
GO
ALTER TABLE [dbo].[LectureTimetable]  WITH CHECK ADD  CONSTRAINT [FK_LectureTimetable_LectureHall] FOREIGN KEY([LocationId])
REFERENCES [dbo].[LectureHall] ([HallId])
GO
ALTER TABLE [dbo].[LectureTimetable] CHECK CONSTRAINT [FK_LectureTimetable_LectureHall]
GO
ALTER TABLE [dbo].[LectureTimetable]  WITH CHECK ADD  CONSTRAINT [FK_LectureTimetable_LectureType] FOREIGN KEY([LectureTypeId])
REFERENCES [dbo].[LectureType] ([LectureTypeId])
GO
ALTER TABLE [dbo].[LectureTimetable] CHECK CONSTRAINT [FK_LectureTimetable_LectureType]
GO
ALTER TABLE [dbo].[LectureTimetable]  WITH CHECK ADD  CONSTRAINT [FK_LectureTimetable_SemesterRegistration] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[SemesterRegistration] ([SemesterId])
GO
ALTER TABLE [dbo].[LectureTimetable] CHECK CONSTRAINT [FK_LectureTimetable_SemesterRegistration]
GO
ALTER TABLE [dbo].[LectureTimetable]  WITH CHECK ADD  CONSTRAINT [FK_LectureTimetable_SemesterSubject] FOREIGN KEY([SemesterSubjectId])
REFERENCES [dbo].[SemesterSubject] ([Id])
GO
ALTER TABLE [dbo].[LectureTimetable] CHECK CONSTRAINT [FK_LectureTimetable_SemesterSubject]
GO
ALTER TABLE [dbo].[LectureTimetableLog]  WITH CHECK ADD  CONSTRAINT [FK_LectureTimetableLog_AspNetUsers] FOREIGN KEY([LecturerId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[LectureTimetableLog] CHECK CONSTRAINT [FK_LectureTimetableLog_AspNetUsers]
GO
ALTER TABLE [dbo].[LectureTimetableLog]  WITH CHECK ADD  CONSTRAINT [FK_LectureTimetableLog_LectureHall] FOREIGN KEY([LocationId])
REFERENCES [dbo].[LectureHall] ([HallId])
GO
ALTER TABLE [dbo].[LectureTimetableLog] CHECK CONSTRAINT [FK_LectureTimetableLog_LectureHall]
GO
ALTER TABLE [dbo].[LectureTimetableLog]  WITH CHECK ADD  CONSTRAINT [FK_LectureTimetableLog_LectureTimetable] FOREIGN KEY([TimetableId])
REFERENCES [dbo].[LectureTimetable] ([TimetableId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LectureTimetableLog] CHECK CONSTRAINT [FK_LectureTimetableLog_LectureTimetable]
GO
ALTER TABLE [dbo].[LectureTimetableLog]  WITH CHECK ADD  CONSTRAINT [FK_LectureTimetableLog_LectureType] FOREIGN KEY([LectureTypeId])
REFERENCES [dbo].[LectureType] ([LectureTypeId])
GO
ALTER TABLE [dbo].[LectureTimetableLog] CHECK CONSTRAINT [FK_LectureTimetableLog_LectureType]
GO
ALTER TABLE [dbo].[LectureTimetableLog]  WITH CHECK ADD  CONSTRAINT [FK_LectureTimetableLog_SemesterRegistration] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[SemesterRegistration] ([SemesterId])
GO
ALTER TABLE [dbo].[LectureTimetableLog] CHECK CONSTRAINT [FK_LectureTimetableLog_SemesterRegistration]
GO
ALTER TABLE [dbo].[LectureTimetableLog]  WITH CHECK ADD  CONSTRAINT [FK_LectureTimetableLog_SemesterSubject] FOREIGN KEY([SemesterSubjectId])
REFERENCES [dbo].[SemesterSubject] ([Id])
GO
ALTER TABLE [dbo].[LectureTimetableLog] CHECK CONSTRAINT [FK_LectureTimetableLog_SemesterSubject]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_AspNetUsers]
GO
ALTER TABLE [dbo].[PaymentRate]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRate_Degree] FOREIGN KEY([DegreeId])
REFERENCES [dbo].[Degree] ([DegreeId])
GO
ALTER TABLE [dbo].[PaymentRate] CHECK CONSTRAINT [FK_PaymentRate_Degree]
GO
ALTER TABLE [dbo].[PaymentRate]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRate_Designation] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[Designation] ([DesignationId])
GO
ALTER TABLE [dbo].[PaymentRate] CHECK CONSTRAINT [FK_PaymentRate_Designation]
GO
ALTER TABLE [dbo].[PaymentRate]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRate_Faculty] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculty] ([FacultyId])
GO
ALTER TABLE [dbo].[PaymentRate] CHECK CONSTRAINT [FK_PaymentRate_Faculty]
GO
ALTER TABLE [dbo].[PaymentRate]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRate_LectureType] FOREIGN KEY([LectureTypeId])
REFERENCES [dbo].[LectureType] ([LectureTypeId])
GO
ALTER TABLE [dbo].[PaymentRate] CHECK CONSTRAINT [FK_PaymentRate_LectureType]
GO
ALTER TABLE [dbo].[PaymentRate]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRate_Specialization] FOREIGN KEY([SpecializationId])
REFERENCES [dbo].[Specialization] ([SpecializationId])
GO
ALTER TABLE [dbo].[PaymentRate] CHECK CONSTRAINT [FK_PaymentRate_Specialization]
GO
ALTER TABLE [dbo].[PaymentRate]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRate_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
GO
ALTER TABLE [dbo].[PaymentRate] CHECK CONSTRAINT [FK_PaymentRate_Subject]
GO
ALTER TABLE [dbo].[PaymentRateLog]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRateLog_Degree] FOREIGN KEY([DegreeId])
REFERENCES [dbo].[Degree] ([DegreeId])
GO
ALTER TABLE [dbo].[PaymentRateLog] CHECK CONSTRAINT [FK_PaymentRateLog_Degree]
GO
ALTER TABLE [dbo].[PaymentRateLog]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRateLog_Designation] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[Designation] ([DesignationId])
GO
ALTER TABLE [dbo].[PaymentRateLog] CHECK CONSTRAINT [FK_PaymentRateLog_Designation]
GO
ALTER TABLE [dbo].[PaymentRateLog]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRateLog_Faculty] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculty] ([FacultyId])
GO
ALTER TABLE [dbo].[PaymentRateLog] CHECK CONSTRAINT [FK_PaymentRateLog_Faculty]
GO
ALTER TABLE [dbo].[PaymentRateLog]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRateLog_LectureType] FOREIGN KEY([LectureTypeId])
REFERENCES [dbo].[LectureType] ([LectureTypeId])
GO
ALTER TABLE [dbo].[PaymentRateLog] CHECK CONSTRAINT [FK_PaymentRateLog_LectureType]
GO
ALTER TABLE [dbo].[PaymentRateLog]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRateLog_PaymentRate] FOREIGN KEY([PaymentRateId])
REFERENCES [dbo].[PaymentRate] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PaymentRateLog] CHECK CONSTRAINT [FK_PaymentRateLog_PaymentRate]
GO
ALTER TABLE [dbo].[PaymentRateLog]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRateLog_Specialization] FOREIGN KEY([SpecializationId])
REFERENCES [dbo].[Specialization] ([SpecializationId])
GO
ALTER TABLE [dbo].[PaymentRateLog] CHECK CONSTRAINT [FK_PaymentRateLog_Specialization]
GO
ALTER TABLE [dbo].[PaymentRateLog]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRateLog_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
GO
ALTER TABLE [dbo].[PaymentRateLog] CHECK CONSTRAINT [FK_PaymentRateLog_Subject]
GO
ALTER TABLE [dbo].[SemesterRegistration]  WITH CHECK ADD  CONSTRAINT [FK_SemesterRegistration_CalendarPeriod] FOREIGN KEY([CalendarPeriodId])
REFERENCES [dbo].[CalendarPeriod] ([Id])
GO
ALTER TABLE [dbo].[SemesterRegistration] CHECK CONSTRAINT [FK_SemesterRegistration_CalendarPeriod]
GO
ALTER TABLE [dbo].[SemesterRegistration]  WITH CHECK ADD  CONSTRAINT [FK_SemesterRegistration_Degree] FOREIGN KEY([DegreeId])
REFERENCES [dbo].[Degree] ([DegreeId])
GO
ALTER TABLE [dbo].[SemesterRegistration] CHECK CONSTRAINT [FK_SemesterRegistration_Degree]
GO
ALTER TABLE [dbo].[SemesterRegistration]  WITH CHECK ADD  CONSTRAINT [FK_SemesterRegistration_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[SemesterRegistration] CHECK CONSTRAINT [FK_SemesterRegistration_Department]
GO
ALTER TABLE [dbo].[SemesterRegistration]  WITH CHECK ADD  CONSTRAINT [FK_SemesterRegistration_Faculty] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculty] ([FacultyId])
GO
ALTER TABLE [dbo].[SemesterRegistration] CHECK CONSTRAINT [FK_SemesterRegistration_Faculty]
GO
ALTER TABLE [dbo].[SemesterRegistration]  WITH CHECK ADD  CONSTRAINT [FK_SemesterRegistration_Institute] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institute] ([InstituteId])
GO
ALTER TABLE [dbo].[SemesterRegistration] CHECK CONSTRAINT [FK_SemesterRegistration_Institute]
GO
ALTER TABLE [dbo].[SemesterRegistration]  WITH CHECK ADD  CONSTRAINT [FK_SemesterRegistration_Intake] FOREIGN KEY([IntakeId])
REFERENCES [dbo].[Intake] ([IntakeId])
GO
ALTER TABLE [dbo].[SemesterRegistration] CHECK CONSTRAINT [FK_SemesterRegistration_Intake]
GO
ALTER TABLE [dbo].[SemesterRegistration]  WITH CHECK ADD  CONSTRAINT [FK_SemesterRegistration_Specialization] FOREIGN KEY([SpecializationId])
REFERENCES [dbo].[Specialization] ([SpecializationId])
GO
ALTER TABLE [dbo].[SemesterRegistration] CHECK CONSTRAINT [FK_SemesterRegistration_Specialization]
GO
ALTER TABLE [dbo].[SemesterSubject]  WITH CHECK ADD  CONSTRAINT [FK_SemesterSubject_SemesterRegistration] FOREIGN KEY([SemesterRegistrationId])
REFERENCES [dbo].[SemesterRegistration] ([SemesterId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SemesterSubject] CHECK CONSTRAINT [FK_SemesterSubject_SemesterRegistration]
GO
ALTER TABLE [dbo].[SemesterSubject]  WITH CHECK ADD  CONSTRAINT [FK_SemesterSubject_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SemesterSubject] CHECK CONSTRAINT [FK_SemesterSubject_Subject]
GO
ALTER TABLE [dbo].[SemesterSubjectLIC]  WITH CHECK ADD  CONSTRAINT [FK_SemesterSubjectLIC_AspNetUsers] FOREIGN KEY([LICId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[SemesterSubjectLIC] CHECK CONSTRAINT [FK_SemesterSubjectLIC_AspNetUsers]
GO
ALTER TABLE [dbo].[SemesterSubjectLIC]  WITH CHECK ADD  CONSTRAINT [FK_SemesterSubjectLIC_SemesterSubject] FOREIGN KEY([SemesterSubjectId])
REFERENCES [dbo].[SemesterSubject] ([Id])
GO
ALTER TABLE [dbo].[SemesterSubjectLIC] CHECK CONSTRAINT [FK_SemesterSubjectLIC_SemesterSubject]
GO
ALTER TABLE [dbo].[Specialization]  WITH CHECK ADD  CONSTRAINT [FK_Specialization_Degree] FOREIGN KEY([DegreeId])
REFERENCES [dbo].[Degree] ([DegreeId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Specialization] CHECK CONSTRAINT [FK_Specialization_Degree]
GO
ALTER TABLE [dbo].[Specialization]  WITH CHECK ADD  CONSTRAINT [FK_Specialization_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[Specialization] CHECK CONSTRAINT [FK_Specialization_Department]
GO
ALTER TABLE [dbo].[Specialization]  WITH CHECK ADD  CONSTRAINT [FK_Specialization_Institute] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institute] ([InstituteId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Specialization] CHECK CONSTRAINT [FK_Specialization_Institute]
GO
ALTER TABLE [dbo].[StudentBatch]  WITH CHECK ADD  CONSTRAINT [FK_StudentBatch_SemesterRegistration] FOREIGN KEY([SemesterRegistrationId])
REFERENCES [dbo].[SemesterRegistration] ([SemesterId])
GO
ALTER TABLE [dbo].[StudentBatch] CHECK CONSTRAINT [FK_StudentBatch_SemesterRegistration]
GO
ALTER TABLE [dbo].[Subject]  WITH CHECK ADD  CONSTRAINT [FK_Subject_Degree] FOREIGN KEY([DegreeId])
REFERENCES [dbo].[Degree] ([DegreeId])
GO
ALTER TABLE [dbo].[Subject] CHECK CONSTRAINT [FK_Subject_Degree]
GO
ALTER TABLE [dbo].[SubWorkflows]  WITH CHECK ADD  CONSTRAINT [FK_SubWorkflows_AspNetRoles] FOREIGN KEY([WorkflowRole])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[SubWorkflows] CHECK CONSTRAINT [FK_SubWorkflows_AspNetRoles]
GO
ALTER TABLE [dbo].[SubWorkflows]  WITH CHECK ADD  CONSTRAINT [FK_SubWorkflows_AspNetUsers] FOREIGN KEY([WorkflowUser])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[SubWorkflows] CHECK CONSTRAINT [FK_SubWorkflows_AspNetUsers]
GO
ALTER TABLE [dbo].[SubWorkflows]  WITH CHECK ADD  CONSTRAINT [FK_SubWorkflows_Workflows] FOREIGN KEY([WorkflowId])
REFERENCES [dbo].[Workflows] ([Id])
GO
ALTER TABLE [dbo].[SubWorkflows] CHECK CONSTRAINT [FK_SubWorkflows_Workflows]
GO
ALTER TABLE [dbo].[Workflows]  WITH CHECK ADD  CONSTRAINT [FK_Workflows_Faculty] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculty] ([FacultyId])
GO
ALTER TABLE [dbo].[Workflows] CHECK CONSTRAINT [FK_Workflows_Faculty]
GO
USE [master]
GO
ALTER DATABASE [PMS] SET  READ_WRITE 
GO
