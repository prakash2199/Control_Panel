USE [Equibase_Centrum]
GO

DROP PROCEDURE [dbo].[sp_MailQueueAttachments_Save]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_MailQueueAttachments_Save]
@MailQueueId char(36),
@Code varchar(36),	 
@AttachmentType char(1),
@Filepath varchar(1000)	 
AS BEGIN

INSERT INTO MailQueAttachments
                         (MailQueueId, Code, AttachmentType, Filepath)

VALUES					 (@MailQueueId, @Code, @AttachmentType, @Filepath)

END
GO


