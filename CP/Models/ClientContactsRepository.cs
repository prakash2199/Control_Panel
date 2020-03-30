using EquiModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CP.Models
{
    public class ClientContactsRepository
    {
        public static int Add(ClientContactViewModel model, string Path)
        {
            return CommonRepository.SendRequest(Path, CommonRepository.SessionId, model);
        }

        public static List<ClientContactViewModel> GetAll(string Status)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Search", Status }
            };
            return CommonRepository.GetListResponse<ClientContactViewModel>("ClientContacts/GetAll", List);
        }

        public static ClientContactViewModel Get(int Id)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Id", Id.ToString() }
            };
            return CommonRepository.GetResponse<ClientContactViewModel>("ClientContacts/Get", List);
        }

        public static int DeleteOrRestore(int Id, string Path)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "ClientContactId", Id.ToString() }
            };
            return CommonRepository.GetResponse<int>(Path, List);
        }

    }
}