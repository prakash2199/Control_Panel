using CP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CP.Controllers
{
    [Authorize]
    public class UploadLogController : Controller
    {
        // GET: UploadLog
        public ActionResult Index()
        {
            try
            {               
                ViewBag.UploadLog  = UploadLogRepository.GetAll();
                ViewBag.FinYear = UploadLogRepository.Finyears();
                return View();

            }
            catch(Exception e)
            {
                return PartialView("Error", e);
            }
        }
    }
}