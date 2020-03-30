USE [Equibase_Centrum]
GO

/****** Object:  UserDefinedTableType [dbo].[ReportCompaniesList]    Script Date: 19/11/2019 12:43:04 ******/
DROP TYPE [dbo].[ReportCompaniesList]
GO

/****** Object:  UserDefinedTableType [dbo].[ReportCompaniesList]    Script Date: 19/11/2019 12:43:04 ******/
CREATE TYPE [dbo].[ReportCompaniesList] AS TABLE(
	[CompanyId] [int] NULL,
	[RatingId] [int] NULL,
	[TargetPrice] [float] NULL,
	[Price] [float] NULL,
	[TargetDate] [datetime] NULL,
	[AdjTargetPrice] [float] NULL,
	[Factor] [float] NULL
)
GO


