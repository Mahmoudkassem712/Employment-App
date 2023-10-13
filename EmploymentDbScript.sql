CREATE DATABASE EmploymentSystemDB2 ;
GO

USE [EmploymentSystemDB2]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/11/2023 10:03:08 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserTypeName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserTypeId] [int] NOT NULL,
	[FullName] [nvarchar](450) NOT NULL,
	[Password] [nvarchar](450) NOT NULL,
	[Email] [nvarchar](450) Not NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [dbo].[Users]
           ([UserTypeId]
           ,[FullName]
           ,[Password]
           ,[Email])
     VALUES
           (1
           ,'Mahmoud Kassem'
           ,'181996'
           ,'mahmoud_kassem@outlook.com')
GO

INSERT INTO [dbo].[Users]
           ([UserTypeId]
           ,[FullName]
           ,[Password]
           ,[Email])
     VALUES
           (2
           ,'Fares Kassem'
           ,'681996'
           ,'fares_kassem@outlook.com')
GO






CREATE TABLE [dbo].[Vacancy](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VacancyDescription] [nvarchar](max) NOT NULL,
	[EmployerId] [int] NOT NULL,
	[MaximumNumberForApplicants] [int] NOT NULL,
	[ExpiryDate] [date] NOT NULL,
	[IsPost] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[IsExpired] [bit] NOT NULL,
 CONSTRAINT [PK_Vacancy] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO




CREATE TABLE [dbo].[ApplicantVacancies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VacancyId] [int] NOT NULL,
	[ApplicantId] [int] NOT NULL,
	[ApplyingDate] [date] NOT NULL,
 CONSTRAINT [PK_ApplicantVacancies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ApplicantVacancies]  WITH CHECK ADD  CONSTRAINT [FK_ApplicantVacancies_Vacancy] FOREIGN KEY([VacancyId])
REFERENCES [dbo].[Vacancy] ([Id])
GO

ALTER TABLE [dbo].[ApplicantVacancies] CHECK CONSTRAINT [FK_ApplicantVacancies_Vacancy]
GO




