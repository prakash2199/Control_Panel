using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EquiModels;

namespace CP.Models
{
    public class CompaniesRepository
    {
        public static List<CompaniesViewModel> GetAll(string Status,string SectorId)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Search", Status },
                { "SectorId",SectorId}
            };
            return CommonRepository.GetListResponse<CompaniesViewModel>("Companies/GetAll", List);
        }

        public static CompaniesViewModel Get(int Id)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Id", Id.ToString() }
            };
            return CommonRepository.GetResponse<CompaniesViewModel>("Companies/Get", List);
        }

        public static int Add(CompaniesViewModel model, string Path)
        {
            return CommonRepository.SendRequest(Path, CommonRepository.SessionId, model);
        }

        public static List<CompaniesViewModel> GetAllCompanies(int? SectorId)
        {
            Dictionary<string, string> CompaniesList = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "SectorId",  SectorId.ToString() }
            };
            return CommonRepository.GetListResponse<CompaniesViewModel>("Companies/GetCompaniesBySector", CompaniesList);
        }

        public static int DeleteOrRestore(long Id, string Path)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "CompanyId", Id.ToString() }
            };
            return CommonRepository.GetResponse<int>(Path, List);
        }

        public static int DeleteCompanyData(long? CompanyDataId, string Path)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "CompanyDataId", CompanyDataId.ToString() }
            };
            return CommonRepository.GetResponse<int>(Path, List);
        }

        public static List<CompaniesViewModel> GetUploadData(string Code)
        {
            Dictionary<string, string> CompanieDataList = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "CompanyCode",  Code }
            };
            return CommonRepository.GetListResponse<CompaniesViewModel>("Companies/GetCompanieUploadData", CompanieDataList);

        }

        public static List<CompaniesViewModel> GetUploaders()
        {
            Dictionary<string, string> UploaderList = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId }
            };
            return CommonRepository.GetListResponse<CompaniesViewModel>("Companies/GetAllUploaders", UploaderList);
        }

        public static List<CompaniesViewModel> GetReportCodes()
        {
            Dictionary<string, string> ReportCodeList = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId }
            };
            return CommonRepository.GetListResponse<CompaniesViewModel>("Companies/GetAllReportCode", ReportCodeList);
        }
        
        public static List<UserViewModel> GetSectorAnalystById(long SectorId,string AnalystType=null)
         {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "SectorId", Convert.ToString(SectorId) },
                { "AnalystType", AnalystType }
            };
            return CommonRepository.GetListResponse<UserViewModel>("Sectors/GetSectorAnalystById", List);
         }
    }

    }
