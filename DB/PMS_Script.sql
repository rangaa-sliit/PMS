USE [master]
GO
/****** Object:  Database [PMS]    Script Date: 8/22/2022 4:18:16 PM ******/
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
/****** Object:  Table [dbo].[Appointment]    Script Date: 8/22/2022 4:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[AppointmentId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](256) NOT NULL,
	[AppointmentTypeId] [int] NOT NULL,
	[AppointmentFrom] [date] NOT NULL,
	[AppointmentTo] [date] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppointmentType]    Script Date: 8/22/2022 4:18:16 PM ******/
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
/****** Object:  Table [dbo].[ApprovalStages]    Script Date: 8/22/2022 4:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApprovalStages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StageName] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[Priority] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ApprovalStages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 8/22/2022 4:18:16 PM ******/
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
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 8/22/2022 4:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](256) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 8/22/2022 4:18:16 PM ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 8/22/2022 4:18:16 PM ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 8/22/2022 4:18:16 PM ******/
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
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](150) NOT NULL,
	[IsActive] [bit] NOT NULL,
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
/****** Object:  Table [dbo].[CalendarPeriod]    Script Date: 8/22/2022 4:18:16 PM ******/
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
/****** Object:  Table [dbo].[Campus]    Script Date: 8/22/2022 4:18:16 PM ******/
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
/****** Object:  Table [dbo].[ConductedLectures]    Script Date: 8/22/2022 4:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConductedLectures](
	[CLId] [int] IDENTITY(1,1) NOT NULL,
	[LecturerId] [nvarchar](256) NOT NULL,
	[LectureDate] [date] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[LectureTypeId] [int] NOT NULL,
	[LectureHallId] [int] NOT NULL,
	[Location] [int] NOT NULL,
	[StudentBatchId] [int] NOT NULL,
	[StudentCount] [int] NOT NULL,
	[StudentAttendanceSheet] [nvarchar](256) NOT NULL,
	[ApprovalStageId] [int] NOT NULL,
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
/****** Object:  Table [dbo].[ConfigurationalSettings]    Script Date: 8/22/2022 4:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConfigurationalSettings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConfigurationKey] [nvarchar](250) NOT NULL,
	[ConfigurationValue] [nvarchar](150) NULL,
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
/****** Object:  Table [dbo].[Degree]    Script Date: 8/22/2022 4:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Degree](
	[DegreeId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](100) NULL,
	[Name] [nvarchar](1000) NULL,
	[Description] [nvarchar](2000) NULL,
	[FacultyId] [int] NULL,
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
/****** Object:  Table [dbo].[Department]    Script Date: 8/22/2022 4:18:16 PM ******/
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
/****** Object:  Table [dbo].[Designation]    Script Date: 8/22/2022 4:18:16 PM ******/
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
/****** Object:  Table [dbo].[Faculty]    Script Date: 8/22/2022 4:18:16 PM ******/
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
/****** Object:  Table [dbo].[Institute]    Script Date: 8/22/2022 4:18:16 PM ******/
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
/****** Object:  Table [dbo].[LectureHall]    Script Date: 8/22/2022 4:18:16 PM ******/
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
/****** Object:  Table [dbo].[LectureType]    Script Date: 8/22/2022 4:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LectureType](
	[LectureTypeId] [int] IDENTITY(1,1) NOT NULL,
	[LectureTypeName] [nvarchar](100) NOT NULL,
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
/****** Object:  Table [dbo].[PaymentRate]    Script Date: 8/22/2022 4:18:16 PM ******/
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
	[RatePerHour] [float] NOT NULL,
	[IsApproved] [bit] NOT NULL,
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
/****** Object:  Table [dbo].[SemesterRegistration]    Script Date: 8/22/2022 4:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SemesterRegistration](
	[SemesterId] [int] IDENTITY(1,1) NOT NULL,
	[AcadamicYear] [int] NOT NULL,
	[AcadamicSemester] [int] NOT NULL,
	[From] [datetime] NOT NULL,
	[To] [datetime] NOT NULL,
	[DegreeId] [int] NOT NULL,
	[CalendarYear] [int] NOT NULL,
	[CalendarPeriod] [int] NOT NULL,
	[FacultyId] [int] NOT NULL,
	[InstituteId] [int] NOT NULL,
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
/****** Object:  Table [dbo].[SemesterSubject]    Script Date: 8/22/2022 4:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SemesterSubject](
	[SemesterRegistrationId] [int] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SemesterSubject] PRIMARY KEY CLUSTERED 
(
	[SemesterRegistrationId] ASC,
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialization]    Script Date: 8/22/2022 4:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialization](
	[SpecializationId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](100) NULL,
	[Name] [nvarchar](1000) NULL,
	[DegreeId] [int] NOT NULL,
	[InstituteId] [int] NOT NULL,
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
/****** Object:  Table [dbo].[StudentBatch]    Script Date: 8/22/2022 4:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentBatch](
	[StudentBatchId] [int] IDENTITY(1,1) NOT NULL,
	[CalendarYear] [int] NOT NULL,
	[CalendarPeriod] [nvarchar](150) NOT NULL,
	[Degree] [nvarchar](1000) NOT NULL,
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
/****** Object:  Table [dbo].[Subject]    Script Date: 8/22/2022 4:18:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectId] [int] IDENTITY(1,1) NOT NULL,
	[SubjectCode] [nvarchar](100) NOT NULL,
	[SubjectName] [nvarchar](256) NOT NULL,
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
/****** Object:  Table [dbo].[Title]    Script Date: 8/22/2022 4:18:16 PM ******/
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
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
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
ALTER TABLE [dbo].[ConductedLectures]  WITH CHECK ADD  CONSTRAINT [FK_ConductedLectures_ApprovalStages] FOREIGN KEY([ApprovalStageId])
REFERENCES [dbo].[ApprovalStages] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConductedLectures] CHECK CONSTRAINT [FK_ConductedLectures_ApprovalStages]
GO
ALTER TABLE [dbo].[ConductedLectures]  WITH CHECK ADD  CONSTRAINT [FK_ConductedLectures_Campus] FOREIGN KEY([Location])
REFERENCES [dbo].[Campus] ([CampusId])
GO
ALTER TABLE [dbo].[ConductedLectures] CHECK CONSTRAINT [FK_ConductedLectures_Campus]
GO
ALTER TABLE [dbo].[ConductedLectures]  WITH CHECK ADD  CONSTRAINT [FK_ConductedLectures_LectureHall] FOREIGN KEY([LectureHallId])
REFERENCES [dbo].[LectureHall] ([HallId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConductedLectures] CHECK CONSTRAINT [FK_ConductedLectures_LectureHall]
GO
ALTER TABLE [dbo].[ConductedLectures]  WITH CHECK ADD  CONSTRAINT [FK_ConductedLectures_LectureType] FOREIGN KEY([LectureTypeId])
REFERENCES [dbo].[LectureType] ([LectureTypeId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConductedLectures] CHECK CONSTRAINT [FK_ConductedLectures_LectureType]
GO
ALTER TABLE [dbo].[ConductedLectures]  WITH CHECK ADD  CONSTRAINT [FK_ConductedLectures_StudentBatch] FOREIGN KEY([StudentBatchId])
REFERENCES [dbo].[StudentBatch] ([StudentBatchId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConductedLectures] CHECK CONSTRAINT [FK_ConductedLectures_StudentBatch]
GO
ALTER TABLE [dbo].[Degree]  WITH CHECK ADD  CONSTRAINT [FK_Degree_Faculty] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculty] ([FacultyId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Degree] CHECK CONSTRAINT [FK_Degree_Faculty]
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
ALTER TABLE [dbo].[PaymentRate]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRate_Degree] FOREIGN KEY([DegreeId])
REFERENCES [dbo].[Degree] ([DegreeId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PaymentRate] CHECK CONSTRAINT [FK_PaymentRate_Degree]
GO
ALTER TABLE [dbo].[PaymentRate]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRate_Designation] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[Designation] ([DesignationId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PaymentRate] CHECK CONSTRAINT [FK_PaymentRate_Designation]
GO
ALTER TABLE [dbo].[PaymentRate]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRate_Faculty] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculty] ([FacultyId])
GO
ALTER TABLE [dbo].[PaymentRate] CHECK CONSTRAINT [FK_PaymentRate_Faculty]
GO
ALTER TABLE [dbo].[PaymentRate]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRate_Specialization] FOREIGN KEY([SpecializationId])
REFERENCES [dbo].[Specialization] ([SpecializationId])
GO
ALTER TABLE [dbo].[PaymentRate] CHECK CONSTRAINT [FK_PaymentRate_Specialization]
GO
ALTER TABLE [dbo].[PaymentRate]  WITH CHECK ADD  CONSTRAINT [FK_PaymentRate_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PaymentRate] CHECK CONSTRAINT [FK_PaymentRate_Subject]
GO
ALTER TABLE [dbo].[SemesterRegistration]  WITH CHECK ADD  CONSTRAINT [FK_SemesterRegistration_Degree] FOREIGN KEY([DegreeId])
REFERENCES [dbo].[Degree] ([DegreeId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SemesterRegistration] CHECK CONSTRAINT [FK_SemesterRegistration_Degree]
GO
ALTER TABLE [dbo].[SemesterRegistration]  WITH CHECK ADD  CONSTRAINT [FK_SemesterRegistration_Faculty] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculty] ([FacultyId])
GO
ALTER TABLE [dbo].[SemesterRegistration] CHECK CONSTRAINT [FK_SemesterRegistration_Faculty]
GO
ALTER TABLE [dbo].[SemesterRegistration]  WITH CHECK ADD  CONSTRAINT [FK_SemesterRegistration_Institute] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institute] ([InstituteId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SemesterRegistration] CHECK CONSTRAINT [FK_SemesterRegistration_Institute]
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
ALTER TABLE [dbo].[Specialization]  WITH CHECK ADD  CONSTRAINT [FK_Specialization_Degree] FOREIGN KEY([DegreeId])
REFERENCES [dbo].[Degree] ([DegreeId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Specialization] CHECK CONSTRAINT [FK_Specialization_Degree]
GO
ALTER TABLE [dbo].[Specialization]  WITH CHECK ADD  CONSTRAINT [FK_Specialization_Institute] FOREIGN KEY([InstituteId])
REFERENCES [dbo].[Institute] ([InstituteId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Specialization] CHECK CONSTRAINT [FK_Specialization_Institute]
GO
USE [master]
GO
ALTER DATABASE [PMS] SET  READ_WRITE 
GO
