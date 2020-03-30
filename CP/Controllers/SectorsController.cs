using EquiModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CP.Models;


namespace CP.Controllers
{
    [Authorize]
    public class SectorsController : Controller
    {
        // GET: Sectors
        public ActionResult Index(string Status = "1")
        {
            try
            {             
                ViewBag.Search = Status;
                return View();
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            try
            {
                TempData["Actionname"] = "Add";
                List<UserViewModel> AnalystList = UsersRepository.GetAnalysts();
                ViewBag.AnalystList = AnalystList.Select(x => new UserViewModel() { Id = x.Id, FullName = x.FullName }).ToList();
                ViewBag.SecondaryAnalystList = null;
                ViewBag.PrimaryAnalystList =new SelectList(AnalystList, "Id","FullName");
                ViewBag.SectorCompanies = null;
                ViewBag.Uploaders = new SelectList(CompaniesRepository.GetUploaders(), "Id", "Name");
                ViewBag.ReportCode = new SelectList(CompaniesRepository.GetReportCodes(), "Id", "Name");
                ViewBag.Errors = null;
                return PartialView();
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }

        [HttpPost]
        public ActionResult Add(SectorViewModel viewModel)
        {
            try
            {
                TempData["Actionname"] = "Add";
                ViewBag.GroupList = new SelectList(LovRepository.GetAll("SectorGroup"), "Value", "Label");
                List<UserViewModel> AnalystList = UsersRepository.GetAnalysts();
                ViewBag.AnalystList = AnalystList.Select(x => new UserViewModel() { Id = x.Id, FullName = x.FullName }).ToList(); 
                ViewBag.PrimaryAnalystList = new SelectList(AnalystList, "Id", "FullName");
                ViewBag.SecondaryAnalystList = (AnalystList).Where(x =>(viewModel.SecondaryAnalystIds != null && viewModel.SecondaryAnalystIds.Contains(x.Id))).ToList();
                ViewBag.Uploaders = new SelectList(CompaniesRepository.GetUploaders(), "Id", "Name");
                ViewBag.ReportCode = new SelectList(CompaniesRepository.GetReportCodes(), "Id", "Name");
                SectorsRepository.Add(viewModel,"Sectors/Add");
                if(CommonRepository.IsError)
                {
                    ViewBag.Errors = CommonRepository.ResponseErrors;
                    return PartialView(viewModel);
                }
                else
                {
                    TempData["message"] = "Success";
                    return RedirectToAction("Add");
                }
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }

        public ActionResult Edit(int Id)
        {
            try
            {
                TempData["Actionname"] = "Edit";
                var response = SectorsRepository.Get(Id);
                ViewBag.GroupList = new SelectList(LovRepository.GetAll("SectorGroup"), "Value", "Label");
                List<UserViewModel> AnalystList = UsersRepository.GetAnalysts();
                ViewBag.AnalystList = AnalystList.Select(x => new UserViewModel() {Id=x.Id,FullName=x.FullName }).ToList();
                ViewBag.PrimaryAnalystList = new SelectList(AnalystList, "Id", "FullName");
                ViewBag.SecondaryAnalystList = AnalystList.Where(x =>(response.SecondaryAnalystIds !=null && response.SecondaryAnalystIds.Contains(x.Id))).ToList();
                ViewBag.Uploaders = new SelectList(CompaniesRepository.GetUploaders(), "Id", "Name");
                ViewBag.ReportCode = new SelectList(CompaniesRepository.GetReportCodes(), "Id", "Name");
                return PartialView("Add", response);
            }
            catch(Exception e)
            {
                return View("Error", e);
            }
            
        }

        [HttpPost]
        public ActionResult Edit(SectorViewModel viewModel)
        {
            try
            {
                TempData["Actionname"] = "Edit";
                ViewBag.GroupList = new SelectList(LovRepository.GetAll("SectorGroup"), "Value", "Label");
                List<UserViewModel> AnalystList = UsersRepository.GetAnalysts();
                ViewBag.AnalystList = AnalystList.Select(x => new UserViewModel() { Id = x.Id, FullName = x.FullName }).ToList();
                ViewBag.PrimaryAnalystList = new SelectList(AnalystList, "Id", "FullName");
                ViewBag.SecondaryAnalystList = (AnalystList).Where(x =>(viewModel.SecondaryAnalystIds != null && viewModel.SecondaryAnalystIds.Contains(x.Id)));
                ViewBag.Uploaders = new SelectList(CompaniesRepository.GetUploaders(), "Id", "Name");
                ViewBag.ReportCode = new SelectList(CompaniesRepository.GetReportCodes(), "Id", "Name");
                SectorsRepository.Add(viewModel, "Sectors/Edit");
                if (CommonRepository.IsError)
                {
                    ViewBag.Errors = CommonRepository.ResponseErrors;
                    return PartialView("Add", viewModel);
                }
                else
                {
                    TempData["message"] = "Success";
                    return RedirectToAction("Edit", new { Id = viewModel.Id });
                }
            }
            catch(Exception e)
            {
                return View("Error", e);
            }
           
        }

        public ActionResult DeleteOrRestore(int Id,string Mode)
        {
            try
            {
                string Path = null;
                if (Mode == "Delete")
                { Path = "Sectors/Delete"; }
                else Path = "Sectors/Restore";
                SectorsRepository.DeleteOrRestore(Id, Path);
                if (CommonRepository.IsError)
                {
                    ViewBag.Errors= CommonRepository.ResponseErrors;
                }
                TempData["message"] = CommonRepository.StatusMessage;
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View("Error", e);
            }
          
    }
        [HttpGet]
        public JsonResult GetAll(string Status = "1", string SectorId = null)
        {
            var data = SectorsRepository.GetAll(Status);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
