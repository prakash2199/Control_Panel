USE [Equibase_Centrum]
GO

DROP PROCEDURE [dbo].[sp_ResearchReport_UpdateStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--- Delete				=> sp_Companies_Delete
create PROC [dbo].[sp_ResearchReport_UpdateStatus]
	@Id int,
	@Status int,
	@SessionId nvarchar(100)
as begin
	update ResearchReports set Status = @Status, SessionId = @SessionId, EnteredOn = getdate()
	where Id = @Id
end
GO


