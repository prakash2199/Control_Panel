USE [Equibase_Centrum]
GO

DROP PROCEDURE [dbo].[sp_ResearchReports_GetCompanyData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[sp_ResearchReports_GetCompanyData]
	@SessionId nvarchar(100),
	@CompanyId int
as begin

declare @Ticker nvarchar(100)
set @Ticker=(select Code from companies where id=@CompanyId)
select * from ItemValues where Ticker=@Ticker and ItemCode in ('Rating','PRICE_TARGET','CMP','PRICE_TARGET_END_DATE')

end
GO


