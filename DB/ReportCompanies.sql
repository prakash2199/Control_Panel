USE [Equibase_Centrum]
GO

/****** Object:  Table [dbo].[ReportCompanies]    Script Date: 18/11/2019 12:35:11 ******/
DROP TABLE [dbo].[ReportCompanies]
GO

/****** Object:  Table [dbo].[ReportCompanies]    Script Date: 18/11/2019 12:35:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReportCompanies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReportId] [int] NULL,
	[CompanyId] [int] NULL,
	[RatingId] [int] NULL,
	[TargetPrice] [float] NULL,
	[Price] [float] NULL,
	[TargetDate] [datetime] NULL,
	[AdjTargetPrice] [float] NULL,
	[Factor] [float] NULL,
	[Status] [int] NULL,
	[SessionId] [nvarchar](100) NULL,
	[EnteredOn] [datetime] NULL,
 CONSTRAINT [PK_ReportCompanies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


