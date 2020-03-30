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
    public class ReportItemsController : Controller
    {
        // GET: ReportItems
        public ActionResult Index()
        {
            try
            {
                ViewBag.SectorGroup = CompaniesRepository.GetReportCodes();
                ViewBag.SectorReportItems = ReportItemsRepository.GetSectorReportItems();
                ViewBag.CompanieReportItems = ReportItemsRepository.GetCompanyReportItems();
                return View();
            }
            catch(Exception e)
            {
                return View("Error", e);
            }
        }
        [HttpGet]
        public ActionResult GetReportItems(string ReportGroup,string ReportCode,string ParentReportCode, string Category,string Entity)
        {
            try
            {
                ReportItemsViewModel model = new ReportItemsViewModel();
                var ReportCategory = ReportItemsRepository.GetReportItemsCategory(ReportCode); 
                model.Category =((Category == null)||(Category=="")) ? ReportCategory[0] : Category;
                model.ReportCode = ReportCode;
                model.ReportItems = ReportItemsRepository.GetReportItemsByCategory(model.Category, model.ReportCode);
                model.ReportType = model.ReportItems.Count > 0 ? model.ReportItems[0].ReportType : " ";
                ViewBag.Items = ItemRepository.GetAll(null).Select(x=>x.Code);
                ViewBag.FormatGroups = FormatGroupRepository.GetAll("1").Select(x=>x.FormatGroup);
                ViewBag.ItemTypes = LovRepository.GetAll("Item Type").Select(x=>x.Label);              
                ViewBag.ScaleList = LovRepository.GetAllScale().Select(x=>x.Scale);
                ViewBag.ReportCategory = ReportCategory;              
                model.ParentReportCode = ParentReportCode;
                model.ReportGroup = ReportGroup;
                return PartialView("GetReportItems", model);
            }
            catch(Exception e)
            {
                return PartialView("Error", e);
            }

        }
        [HttpPost]
        public ActionResult GetReportItems(ReportItemsViewModel model)
        {
            try
            {
                var ReportCategory = ReportItemsRepository.GetReportItemsCategory(model.ReportCode);
                model.ParentReportCode = model.ParentReportCode;
                ViewBag.ReportCategory = ReportCategory;
                ViewBag.Items = ItemRepository.GetAll(null).Select(x => x.Code);
                ViewBag.FormatGroups = FormatGroupRepository.GetAll("1").Select(x => x.FormatGroup);                 
                ReportItemsRepository.Add(model, "ReportItems/Save");
                if (CommonRepository.IsError)
                {
                    ViewBag.Errors = CommonRepository.ResponseErrors;
                    return PartialView(model);
                }
                else
                {
                    TempData["message"] = CommonRepository.StatusMessage;
                    return RedirectToAction("GetReportItems",model);
                }
            }
            catch (Exception e)
            {
                return PartialView("Error", e);
            }
        }

        public ActionResult AddSectorReportItems(ReportItemsViewModel model)
        {
            try
            {
                model.ReportCode = "SC_" + model.ReportItemType;
                model.ParentReportCode = model.ReportGroup;
                ReportItemsRepository.AddSecReportItems(model, "ReportItems/AddReportItems");
                return RedirectToAction("GetReportItems",model);
            }
            catch (Exception e)
            {
                return PartialView("Error", e);
            }
        }
        public ActionResult AddCompanyReportItems(ReportItemsViewModel model)
        {
            try
            {
                model.ReportCode = "CC_" + model.ReportItemType;
                model.ParentReportCode = model.ReportGroup;
                ReportItemsRepository.AddCmpReportItems(model, "ReportItems/AddReportItems");
                return RedirectToAction("GetReportItems", model);
            }
            catch (Exception e)
            {
                return PartialView("Error", e);
            }

        }

        [HttpGet]
        public ActionResult DeleteReportItems(string reportcode)
        {
            ReportItemsRepository.Delete(reportcode);
            TempData["message"] = "Report items deleted successfully.";
            return RedirectToAction("Index");
        }
      
    }
}