using EquiModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CP.Models
{
    public class UsersRepository
    {
        public static List<UserViewModel> GetAnalysts()
        {
            Dictionary<string, string> AnalystList = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId},
                {"Status","1"}
            };
            return CommonRepository.GetListResponse<UserViewModel>("Users/GetAllAnalysts", AnalystList);
        }

        public static List<UserViewModel> GetAll(string Status)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Search", Status }
            };
            return CommonRepository.GetListResponse<UserViewModel>("Users/GetAll", List);
        }

        public static int Add(UserViewModel model,string Path)
        {
            
            return CommonRepository.SendRequest(Path, CommonRepository.SessionId, model);
        }

        public static string GetNextUserId( string Path)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId }
            };
            return CommonRepository.GetResponse<string>(Path, List);
        }

        public static UserViewModel Get(int Id)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Id", Id.ToString() }
            };
            return CommonRepository.GetResponse<UserViewModel>("Users/Get", List);
        }
        public static int DeleteOrRestore(int Id, string Path)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "UserId", Id.ToString() }
            };
            return CommonRepository.GetResponse<int>(Path, List);
        }
    }
}