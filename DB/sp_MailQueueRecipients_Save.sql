USE [Equibase_Centrum]
GO

DROP PROCEDURE [dbo].[sp_MailQueueRecipients_Save]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_MailQueueRecipients_Save]
@MailQueueId char(36),
@Code char(36),	 
@RecipientType char(1),
@RecipientName varchar(500),
@RecipientEmail	varchar(1000),	 
@BccBatchNo	int,	 
@CreatedOn datetime,
@Priority int
AS BEGIN

INSERT INTO MailQueueRecipients
                  (MailQueueId, Code, RecipientType, RecipientName, RecipientEmail, BccBatchNo, Createdon, Priority)
VALUES       
                  (@MailQueueId,@Code,@RecipientType,@RecipientName,@RecipientEmail,@BccBatchNo,GETDATE(),@Priority)
				   
END 
GO


