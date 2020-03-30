USE [Equibase_Centrum]
GO

/****** Object:  Table [dbo].[ResearchReports]    Script Date: 18/11/2019 12:34:36 ******/
DROP TABLE [dbo].[ResearchReports]
GO

/****** Object:  Table [dbo].[ResearchReports]    Script Date: 18/11/2019 12:34:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ResearchReports](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PDF] [nvarchar](200) NOT NULL,
	[ProductId] [int] NOT NULL,
	[ReportDate] [datetime] NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Subject] [nvarchar](250) NOT NULL,
	[Note] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[SessionId] [nvarchar](100) NULL,
	[EnteredOn] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


