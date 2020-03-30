USE [Equibase_Centrum]
GO

DROP PROCEDURE [dbo].[sp_MailQueue_Save]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_MailQueue_Save]
@Id char(36),
@FromName varchar(250),
@FromEmail varchar(250),
@MailSubject text,
@IsHtml	bit,
@HtmlBody text,
@CreatedOn datetime,
@CreatedBy int,
@MailSent bit,
@MailSentOn	datetime,
@CC	varchar(1000),
@BCC varchar(1000),
@Priority bit,
@RetryAttempts int,
@FailureMessage	text,
@BccBatchSize int,
@MailDelay int,
@Active	bit,
@ReportId int 

AS BEGIN


INSERT INTO MailQueue
                         (Id,FromName,FromEmail,MailSubject,IsHtml,HtmlBody,CreatedOn,CreatedBy,MailSent,MailSentOn,CC,
						  BCC,Priority,RetryAttempts,FailureMessage,BccBatchSize,MailDelay,Active,ReportId)

VALUES                   (@Id,@FromName,@FromEmail,@MailSubject,@IsHtml,@HtmlBody,GETDATE(),@CreatedBy,@MailSent,@MailSentOn,@CC,
						  @BCC,@Priority,@RetryAttempts,@FailureMessage,@BccBatchSize,@MailDelay,@Active,@ReportId)

END




GO


