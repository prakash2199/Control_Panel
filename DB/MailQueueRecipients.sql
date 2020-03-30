USE [Equibase_Centrum]
GO

ALTER TABLE [dbo].[MailQueueRecipients] DROP CONSTRAINT [DF_MailQueueRecipients_active]
GO

DROP TABLE [dbo].[MailQueueRecipients]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MailQueueRecipients](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MailQueueId] [char](36) NULL,
	[Code] [char](36) NULL,
	[RecipientType] [char](1) NULL,
	[RecipientName] [varchar](500) NULL,
	[RecipientEmail] [varchar](1000) NULL,
	[BccBatchNo] [int] NULL,
	[Createdon] [datetime] NULL,
	[SentOn] [datetime] NULL,
	[Delivered] [bit] NULL,
	[DeliveredOn] [datetime] NULL,
	[RetryAttempts] [int] NULL,
	[Failed] [int] NULL,
	[FailureMessage] [text] NULL,
	[Priority] [int] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK__MailQueu__3213E83F235B3F1E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[MailQueueRecipients] ADD  CONSTRAINT [DF_MailQueueRecipients_active]  DEFAULT ((1)) FOR [active]
GO


