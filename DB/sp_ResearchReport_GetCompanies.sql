USE [Equibase_Centrum]
GO

/****** Object:  StoredProcedure [dbo].[sp_ResearchReport_GetCompanies]    Script Date: 21/11/2019 17:03:46 ******/
DROP PROCEDURE [dbo].[sp_ResearchReport_GetCompanies]
GO

/****** Object:  StoredProcedure [dbo].[sp_ResearchReport_GetCompanies]    Script Date: 21/11/2019 17:03:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROC [dbo].[sp_ResearchReport_GetCompanies]
@SessionId nvarchar(100),
@ReportId int
as begin
	begin try
	select *  from ReportCompanies where ReportId=@ReportId
	end try
	begin catch
		select null Id
	end catch
end
GO


