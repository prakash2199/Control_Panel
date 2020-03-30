using EquiModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CP.Models
{
    public class ClientsRepository
    {
        public static List<UserViewModel> GetAllSalesPerson()
        {
            Dictionary<string, string> SalesPersonList = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId},
                {"Status","1"}
            };
            return CommonRepository.GetListResponse<UserViewModel>("Clients/GetAllSalesPerson", SalesPersonList);
        }

        public static ClientViewModel Get(int Id)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Id", Id.ToString() }
            };
            return CommonRepository.GetResponse<ClientViewModel>("Clients/Get", List);
        }

        public static List<ClientViewModel> GetAll(string Status)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Search", Status }
            };
            return CommonRepository.GetListResponse<ClientViewModel>("Clients/GetAll", List);
        }
        public static int Add(ClientViewModel model, string Path)
        {
            return CommonRepository.SendRequest(Path, CommonRepository.SessionId, model);
        }

        public static int DeleteOrRestore(int Id, string Path)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "ClientId", Id.ToString() }
            };
            return CommonRepository.GetResponse<int>(Path, List);
        }
    }
}