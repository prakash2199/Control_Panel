using CP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EquiModels;
using System.Web.Security;

namespace CP.Controllers
{
    public class AuthenticateController : Controller
    {
        // GET: Authenticate
        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                var response = AuthenticationRepository.AuthenticateUser();
                if (response != null)
                {
                    FormsAuthentication.SetAuthCookie(response.Username, false);
                    Session["User"] = response;
                    var CheckLicence = AuthenticationRepository.ValidateLicense();
                    if (CheckLicence.IsValid == false)
                    {
                        return Content("License has been expired. please renew the license to continue");
                    }
                    else
                    {
                        response.LicenseMessage = CheckLicence.Message;
                        return RedirectToAction("Index", "Home");
                    }   
                }
                else
                {
                    return View("NotAuthorize");
                }

            }
            catch (Exception ex)
            {
                return Content("Some error occured");
            }

        }
        

    }
}
