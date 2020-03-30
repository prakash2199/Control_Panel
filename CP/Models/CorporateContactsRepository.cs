using EquiModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CP.Models
{
    public class CorporateContactsRepository
    {
        public static int Add(CorporateContactViewModel model, string Path)
        {
            return CommonRepository.SendRequest(Path, CommonRepository.SessionId, model);
        }

        public static List<CorporateContactViewModel> GetAll(string Status)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Search", Status }
            };
            return CommonRepository.GetListResponse<CorporateContactViewModel>("CorporateContacts/GetAll", List);
        }

        public static int DeleteOrRestore(int Id, string Path)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "CorporateContactId", Id.ToString() }
            };
            return CommonRepository.GetResponse<int>(Path, List);
        }

        public static CorporateContactViewModel Get(int Id)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Id", Id.ToString() }
            };
            return CommonRepository.GetResponse<CorporateContactViewModel>("CorporateContacts/Get", List);
        }
    }
}