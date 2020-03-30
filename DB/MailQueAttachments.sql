USE [Equibase_Centrum]
GO

DROP TABLE [dbo].[MailQueAttachments]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MailQueAttachments](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MailQueueId] [char](36) NULL,
	[Code] [varchar](36) NULL,
	[AttachmentType] [char](1) NULL,
	[Filepath] [varchar](1000) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


