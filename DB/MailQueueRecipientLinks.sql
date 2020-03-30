USE [Equibase_Centrum]
GO

DROP TABLE [dbo].[MailQueueRecipientLinks]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MailQueueRecipientLinks](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MailQueueId] [char](36) NULL,
	[RecipientId] [bigint] NULL,
	[Code] [char](36) NULL,
	[ReportId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


