USE [Equibase_Centrum]
GO

/****** Object:  Table [dbo].[ReportSectors]    Script Date: 18/11/2019 12:34:03 ******/
DROP TABLE [dbo].[ReportSectors]
GO

/****** Object:  Table [dbo].[ReportSectors]    Script Date: 18/11/2019 12:34:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ReportSectors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReportId] [int] NULL,
	[SectorId] [int] NULL,
	[Status] [int] NULL,
	[SessionId] [nvarchar](100) NULL,
	[EnteredOn] [datetime] NULL,
 CONSTRAINT [PK_ReportSectors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


