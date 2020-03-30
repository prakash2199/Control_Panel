USE [Equibase_Centrum]
GO

/****** Object:  StoredProcedure [dbo].[sp_ResearchReports_GetAll]    Script Date: 20/11/2019 13:00:17 ******/
DROP PROCEDURE [dbo].[sp_ResearchReports_GetAll]
GO

/****** Object:  StoredProcedure [dbo].[sp_ResearchReports_GetAll]    Script Date: 20/11/2019 13:00:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[sp_ResearchReports_GetAll]
	@SessionId nvarchar(100),
	@Status nvarchar(100)
as begin

 SELECT ROW_NUMBER() OVER(ORDER BY rp.enteredon desc) AS SerialNo,rp.ReportDate ,l.Label as [Product],rp.Subject,rp.Status from ResearchReports rp
 inner join LovMaster l on l.Value=rp.ProductId 
 where l.Category='Products' and l.SubCategory='Website' and rp.Status=@Status

end
GO


