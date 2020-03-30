using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EquiModels;
using System.Web.Security;
using System.Net;
using System.Net.Sockets;

namespace CP.Models
{
    public class AuthenticationRepository
    {
         public static UserViewModel AuthenticateUser()
        {
            string UserName = System.Web.HttpContext.Current.User.Identity.Name;
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                {"UserName", UserName }

            }; 
            return CommonRepository.GetResponse<UserViewModel>("Authenticate/ValidateUser", List);
        }

        public static List<UserViewModel> GetRoleByUserName(string UserName)
        {

            Dictionary<string, string> List = new Dictionary<string, string>
            {
                {"UserName", UserName }

            };
            return CommonRepository.GetListResponse<UserViewModel>("Authenticate/GetRoleByUserName", List);
        }

        public static LicenseViewModel ValidateLicense()
        {
            Dictionary<string, string> List = new Dictionary<string, string>
            {
                {"SessionId", CommonRepository.SessionId },
                {"IsControlPanel","True" }

            };
            return CommonRepository.GetResponse<LicenseViewModel>("Authenticate/CheckLicense", List);
        }


       
      



       
       







    }
}