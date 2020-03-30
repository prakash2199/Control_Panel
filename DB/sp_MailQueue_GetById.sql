USE [Equibase_Centrum]
GO

/****** Object:  StoredProcedure [dbo].[sp_MailQueue_Get]    Script Date: 05/12/2019 11:22:52 ******/
DROP PROCEDURE [dbo].[sp_MailQueue_GetById]
GO

/****** Object:  StoredProcedure [dbo].[sp_MailQueue_Get]    Script Date: 05/12/2019 11:22:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROC [dbo].[sp_MailQueue_GetById]
@Id char(36)
AS BEGIN

SELECT * FROM MailQueue
WHERE Id=@Id

END
GO


