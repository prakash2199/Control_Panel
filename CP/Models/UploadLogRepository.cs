using EquiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP.Models
{
    public class UploadLogRepository
    {
        public static List<int> Finyears()
        {
            List<int> _years = new List<int>();

            _years.Add(DateTime.Now.Year - 1);
            _years.Add(DateTime.Now.Year);
            _years.Add(DateTime.Now.Year + 1);
            _years.Add(DateTime.Now.Year + 2);
            return _years;
        }

        public static List<UploadLogViewModel> GetAll()
        {
            var Years = Finyears();
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                {"SessionId",CommonRepository.SessionId },
                {"FinYear1", Convert.ToString(Years[0])},
                {"FinYear2", Convert.ToString(Years[1]) },
                {"FinYear3", Convert.ToString(Years[2]) },
                {"FinYear4", Convert.ToString(Years[3]) }
            };
            return CommonRepository.GetListResponse<UploadLogViewModel>("Companies/GetUploadLog", List);
        }
    }
}