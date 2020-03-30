USE [Equibase_Centrum]
GO

/****** Object:  StoredProcedure [dbo].[sp_ResearchReport_Add]    Script Date: 21/11/2019 18:02:29 ******/
DROP PROCEDURE [dbo].[sp_ResearchReport_Update]
GO

/****** Object:  StoredProcedure [dbo].[sp_ResearchReport_Add]    Script Date: 21/11/2019 18:02:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROC [dbo].[sp_ResearchReport_Update]
	@PDF NVARCHAR(200),
	@ProductId int,
	@ReportDate datetime,
	@Title nvarchar(250),
	@Subject nvarchar(250),
	@Note nvarchar(max),
	@SessionId nvarchar(100),
	@SectorList dbo.IDList readonly,
	@CompaniesList dbo.ReportCompaniesList readonly,
	@AnalystList dbo.ReportAnalystList readonly
as begin
	begin try
	declare @Id int
	update ResearchReports set  PDF=@PDF,ProductId= @ProductId,ReportDate=@ReportDate,Title=@Title,Subject=@Subject,Note=@Note,SessionId=@SessionId,EnteredOn=GETDATE()
	select @Id = SCOPE_IDENTITY()
		
		delete from ReportCompanies where ReportId = @Id
		delete from ReportSectors where ReportId = @Id
		delete from ReportAnalysts where ReportId = @Id

			if (EXISTS (SELECT 1 FROM @SectorList))
			begin
			insert into ReportSectors(ReportId,SectorId,Status,SessionId,EnteredOn)
			select @Id,id,1,@SessionId,getdate() from @SectorList
			end

			if (EXISTS (SELECT 1 FROM @CompaniesList))
			begin
			insert into ReportCompanies(ReportId,CompanyId,RatingId,TargetPrice,price,TargetDate,AdjTargetPrice,Factor,Status,SessionId,EnteredOn)
			select @Id,CompanyId,RatingId,TargetPrice,Price,TargetDate,AdjTargetPrice,Factor,1,@SessionId,getdate() from @CompaniesList
			end

			if (EXISTS (SELECT 1 FROM @AnalystList))
			begin
			insert into ReportAnalysts(ReportId,AnalystId,Type,Status,SessionId,EnteredOn)
			select @Id,AnalystId,Type,1,@SessionId,getdate() from @AnalystList
			end

	end try
	begin catch
		select null Id
	end catch
end
GO


