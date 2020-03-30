using EquiModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CP.Models
{
    public class ResearchReportRepository
    {
        public static List<ResearchReportViewModel> GetAll(string Status)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Search", Status }
            };
            return CommonRepository.GetListResponse<ResearchReportViewModel>("ResearchReports/GetAll", List);
        }

        public static int DeleteOrRestore(int Id, string Path)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Id", Id.ToString() }
            };
            return CommonRepository.GetResponse<int>(Path, List);
        }

        public static int Add(ResearchReportViewModel model, string Path)
        {
            return CommonRepository.SendRequest(Path, CommonRepository.SessionId,model);
        }

        public static List<ReportCompanyData> GetCompanyData(string CompanyId)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "CompanyId", CompanyId }
            };
            return CommonRepository.GetListResponse<ReportCompanyData>("ResearchReports/GetCompanyData", List);
        }

        public static bool DeleteReportFile(string Path, string PDFName)
        {
            
            try
            {
                System.IO.File.Delete(Path + PDFName);
            }
            catch (Exception EX) { }
            return true;
        }

        public static ResearchReportViewModel Get(int Id)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Id", Id.ToString() }
            };
            return CommonRepository.GetResponse<ResearchReportViewModel>("ResearchReports/Get", List);
        }

        public static int Edit(ResearchReportViewModel model, string Path)
        {
            return CommonRepository.SendRequest(Path, CommonRepository.SessionId, model);
        }

        public static List<DistributionList> GetDistributionList(string ReportId)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "ReportId", ReportId }
            };
            return CommonRepository.GetListResponse<DistributionList>("ResearchReports/DistributionList", List);
        }

        public static string SendBulkMail(MailingQueueViewModel model, string Path)
        {
            return CommonRepository.SendMailRequest(Path, CommonRepository.SessionId, model);
        }
    }

   
}