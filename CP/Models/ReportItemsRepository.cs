using EquiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP.Models
{
    public class ReportItemsRepository
    {

        public static List<SectorViewModel> GetSectorReportItems()
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId }

            };
            return CommonRepository.GetListResponse<SectorViewModel>("ReportItems/GetSectorReportItems", List);
        }

        public static List<CompaniesViewModel> GetCompanyReportItems()
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId }
             
            };
            return CommonRepository.GetListResponse<CompaniesViewModel>("ReportItems/GetCompanyReportItems", List);
        }

        public static List<string> GetReportItemsCategory(string ReportGroup)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                {"ReportGroup",ReportGroup }

            };
            return CommonRepository.GetListResponse<string>("ReportItems/GetReportCategoryBygroup", List);
        }

        public static List<ReportItemsViewModel> GetReportItemsByCategory(string Category,string ReportCode)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                {"Category",Category },
                {"ReportCode",ReportCode }

            };
            return CommonRepository.GetListResponse<ReportItemsViewModel>("ReportItems/GetReportItemsByCategory", List);
        }

        public static int Add(ReportItemsViewModel model, string Path)
        {
            return CommonRepository.SendRequest(Path, CommonRepository.SessionId, model);
        }

        public static int Delete (string reportcode)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                {"ReportCode",reportcode }

            };
            return CommonRepository.GetResponse<int>("ReportItems/Delete",List);
        }

        public static int AddSecReportItems(ReportItemsViewModel model,string Path)
        {
            return CommonRepository.SendRequest(Path, CommonRepository.SessionId, model);
        }

        public static int AddCmpReportItems(ReportItemsViewModel model,string Path)
        {
            string ReportGroup = "";
            var a = SectorsRepository.GetSectorByCompany(model.ReportItemType).ToList();
            string SectorName = SectorsRepository.GetSectorByCompany(model.ReportItemType).Select(x => x.Name).FirstOrDefault();
            ReportGroup = "SC_" + SectorName;
            var SectorReportItems = GetSectorReportItems().Where(x=>x.ReportGroup==ReportGroup).ToList();
            if (SectorReportItems.Count > 0) { model.ReportGroup = ReportGroup; }
            return CommonRepository.SendRequest(Path, CommonRepository.SessionId, model);

        }
    }
}