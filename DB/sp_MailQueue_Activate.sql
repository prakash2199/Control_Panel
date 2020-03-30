USE [Equibase_Centrum]
GO

DROP PROCEDURE [dbo].[sp_MailQueue_Activate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_MailQueue_Activate]
@Id char(36)
AS BEGIN

UPDATE MailQueue SET active = 1 WHERE Id = @Id

END
GO


