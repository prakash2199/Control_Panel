USE [Equibase_Centrum]
GO

DROP TABLE [dbo].[MailQueueRecipientLinkClicks]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MailQueueRecipientLinkClicks](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RecipientLinkId] [int] NULL,
	[ClickedOn] [datetime] NULL,
	[IpAddress] [varchar](100) NULL,
	[UserAgent] [text] NULL,
	[UserDevice] [text] NULL,
	[UserPlatform] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


