using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EquiModels;
namespace CP.Models
{
    public class FormatGroupRepository
    {
        public static List<FormatGroupViewModel> GetAll(string Status)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                {"Status", Status}
            };
            return CommonRepository.GetListResponse<FormatGroupViewModel>("FormatGroup/GetFormatGrops", List);
        }

        public static int Add(FormatGroupViewModel model, string Path)
        {
            return CommonRepository.SendRequest(Path, CommonRepository.SessionId, model);
        }

        public static FormatGroupViewModel Get(long Id)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Id", Id.ToString() }
            };
            return CommonRepository.GetResponse<FormatGroupViewModel>("FormatGroup/Get", List);
        }

        public static int DeleteOrRestore(long Id, string Path)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Id", Id.ToString() }
            };
            return CommonRepository.GetResponse<int>(Path, List);
        }
    }
}