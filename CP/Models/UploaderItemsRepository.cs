using EquiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CP.Models
{
    public class UploaderItemsRepository
    {
        public static List<UploaderItemsViewModel> GetAll(string UploaderCode)
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                { "SessionId", CommonRepository.SessionId },
                { "Search", UploaderCode }
            };
            return CommonRepository.GetListResponse<UploaderItemsViewModel>("Uploader/GetAll", List);
        }
    }
}