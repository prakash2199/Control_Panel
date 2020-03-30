using CP.Models;
using EquiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CP.Controllers
{
     [Authorize]
    public class UploaderItemsController : Controller
    {
        // GET: UploaderItems
        public ActionResult Index()
        {
            try
            {
                ViewBag.Uploaders = new SelectList(CompaniesRepository.GetUploaders(), "Code", "Name");
                UploaderItemsViewModel model = new UploaderItemsViewModel();
                return View(model);
            }
            catch(Exception e)
            {
                return PartialView("Error", e);

            }
        }
        [HttpGet]
        public JsonResult GetAllUploadItemsList(string Status)
        {
            var data = UploaderItemsRepository.GetAll(Status);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}