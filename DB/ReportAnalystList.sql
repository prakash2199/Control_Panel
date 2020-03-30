USE [Equibase_Centrum]
GO

/****** Object:  UserDefinedTableType [dbo].[ReportAnalystList]    Script Date: 19/11/2019 12:50:17 ******/
DROP TYPE [dbo].[ReportAnalystList]
GO

/****** Object:  UserDefinedTableType [dbo].[ReportAnalystList]    Script Date: 19/11/2019 12:50:17 ******/
CREATE TYPE [dbo].[ReportAnalystList] AS TABLE(
	[AnalystId] [int] NULL,
	[Type] [char](1) NULL
)
GO


