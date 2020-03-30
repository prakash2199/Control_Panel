using EquiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP.Models
{
    public static class SectorsRepository
    {
        public static List<SectorViewModel> GetAll(string Status)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Search", Status }
            };
            return CommonRepository.GetListResponse<SectorViewModel>("Sectors/GetAll", List);
        }

        public static SectorViewModel Get(int Id)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Id", Id.ToString() }
            };
            return CommonRepository.GetResponse<SectorViewModel>("Sectors/Get", List);
        }

        public static int Add(SectorViewModel model,string Path)
        {
            return CommonRepository.SendRequest(Path, CommonRepository.SessionId, model);
        }

        public static int DeleteOrRestore(int Id,string Path)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "SectorId", Id.ToString() }
            };
            return CommonRepository.GetResponse<int>(Path, List);
        }

        public static List<SectorViewModel> GetSectorByCompany(string CmpCode)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "CmpCode", CmpCode }
            };
            return CommonRepository.GetListResponse<SectorViewModel>("Sectors/GetSectorByCompanyCode", List);
        }
        
    }
}