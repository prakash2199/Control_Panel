using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EquiModels;
namespace CP.Models
{
    public class ItemRepository
    {
        public static List<UploaderItemsViewModel> GetAll(string ItemLabel)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Search", ItemLabel }
            };
            return CommonRepository.GetListResponse<UploaderItemsViewModel>("Items/GetAll", List);
        }
    }
}