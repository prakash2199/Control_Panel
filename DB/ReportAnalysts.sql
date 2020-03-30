USE [Equibase_Centrum]
GO

/****** Object:  Table [dbo].[ReportAnalysts]    Script Date: 18/11/2019 12:36:15 ******/
DROP TABLE [dbo].[ReportAnalysts]
GO

/****** Object:  Table [dbo].[ReportAnalysts]    Script Date: 18/11/2019 12:36:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReportAnalysts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReportId] [int] NULL,
	[AnalystId] [int] NULL,
	[Type] [char](1) NULL,
	[Status] [int] NULL,
	[SessionId] [nvarchar](100) NULL,
	[EnteredOn] [datetime] NULL,
 CONSTRAINT [PK_ReportAnalysts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


