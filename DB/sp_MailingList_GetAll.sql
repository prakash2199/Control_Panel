USE [Equibase_Centrum]
GO
/****** Object:  StoredProcedure [dbo].[sp_MailingList_GetAll]    Script Date: 04/12/2019 10:13:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




ALTER procedure [dbo].[sp_MailingList_GetAll] 
	@SessionId nvarchar(100),
	@ReportId int
as begin
declare @ProductId int
set @ProductId=(select productid from ResearchReports where id=@ReportId)
select c.name as ClientName,cc.email as Email,cc.FirstName +' '+coalesce(cc.LastName,'') as Name,cc.FirstName,cc.LastName from clientcontacts cc
inner join ClientContactProducts cp on cp.ClientContactId=cc.Id
inner join Clients c on c.Id=cc.ClientId
where cp.ProductId=@ProductId
end
