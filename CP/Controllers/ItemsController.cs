using CP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EquiModels;
namespace CP.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        // GET: Items
        public ActionResult Index(string Search=null)
        {
            try
            {          
                return View();
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
           
        }

        public ActionResult UploaderItems(string Search = "Non-Banking")
        {
            try
            { 
                ViewBag.Uploaders = new SelectList(CompaniesRepository.GetUploaders(), "Code", "Name");
                UploaderItemsViewModel model = new UploaderItemsViewModel();
                model.Code = Search;
                return View(model);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }
        [HttpGet]
        public JsonResult GetAllItems()
        {
            var data = ItemRepository.GetAll(null);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
       
        
    }
}