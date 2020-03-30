USE [Equibase_Centrum]
GO

/****** Object:  StoredProcedure [dbo].[sp_ResearchReport_Add]    Script Date: 21/11/2019 12:57:45 ******/
DROP PROCEDURE [dbo].[sp_ResearchReport_Get]
GO

/****** Object:  StoredProcedure [dbo].[sp_ResearchReport_Add]    Script Date: 21/11/2019 12:57:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROC [dbo].[sp_ResearchReport_Get]
@SessionId nvarchar(100),
@Id int
as begin
	begin try
	select * from ResearchReports where id=@Id
	end try
	begin catch
		select null Id
	end catch
end
GO




