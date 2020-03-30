USE [Equibase_Centrum]
GO
/****** Object:  StoredProcedure [dbo].[sp_ResearchReport_GetAnalysts]    Script Date: 21/11/2019 15:29:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





ALTER PROC [dbo].[sp_ResearchReport_GetAnalysts]
@SessionId nvarchar(100),
@ReportId int
as begin
	begin try
	select analystid,Type from ReportAnalysts where ReportId=@ReportId
	end try
	begin catch
		select null Id
	end catch
end
