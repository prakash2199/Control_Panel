USE [Equibase_Centrum]
GO

ALTER TABLE [dbo].[MailQueue] DROP CONSTRAINT [DF__MailQueue__activ__473C8FC7]
GO

DROP TABLE [dbo].[MailQueue]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MailQueue](
	[Id] [char](36) NOT NULL,
	[FromName] [varchar](250) NULL,
	[FromEmail] [varchar](250) NULL,
	[MailSubject] [text] NULL,
	[IsHtml] [bit] NULL,
	[HtmlBody] [text] NULL,
	[TextBody] [text] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[MailSent] [bit] NULL,
	[MailSentOn] [datetime] NULL,
	[CC] [varchar](1000) NULL,
	[BCC] [varchar](1000) NULL,
	[Priority] [bit] NULL,
	[RetryAttempts] [int] NULL,
	[FailureMessage] [text] NULL,
	[BccBatchSize] [int] NULL,
	[MailDelay] [int] NULL,
	[Active] [bit] NULL,
	[ReportId] [int] NULL,
 CONSTRAINT [PK__MailQueue__3213E83FF1C70712] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[MailQueue] ADD  CONSTRAINT [DF__MailQueue__activ__473C8FC7]  DEFAULT ((0)) FOR [active]
GO


