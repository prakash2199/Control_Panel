using EquiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP.Models
{
    public class LovRepository
    {
        private static List<LovViewModel> _data { get; set; }

        public static List<LovViewModel> GetAll(string SubCategory = null, string Category = null)
        {
            if (_data == null)
            {
                Dictionary<string, string> GroupList = new Dictionary<string, string>
                {
                    { "Category", null },
                    { "SubCategory", null },
                    { "SessionId", CommonRepository.SessionId }
                };
                _data = CommonRepository.GetListResponse<LovViewModel>("Lov/GetAll", GroupList);
            }
            return _data.Where(x => (Category == null || x.Category == Category) && (SubCategory == null || x.SubCategory == SubCategory)).ToList();
        }

        public static List<LovViewModel> GetAllScale()
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId }

            };
            return CommonRepository.GetListResponse<LovViewModel>("Lov/GetAllScale", List);
        }
    }
}