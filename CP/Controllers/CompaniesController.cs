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
    public class CompaniesController : Controller
    {
        // GET: Companies
        public ActionResult Index()
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

        [HttpGet]
        public ActionResult Add()
        {
            try
            {
                CompaniesViewModel model = new CompaniesViewModel();
                TempData["Actionname"] = "Add";
                ViewBag.countryList = new SelectList(LovRepository.GetAll("countries", "Clients").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.SectorsList = new SelectList(SectorsRepository.GetAll(Status: "1"), "Id", "Name");
                List<UserViewModel> AnalystList = UsersRepository.GetAnalysts();
                ViewBag.AnalystList = AnalystList.Select(x => new UserViewModel() { Id = x.Id, FullName = x.FullName }).ToList();
                ViewBag.SecondaryAnalystList = null;
                ViewBag.PrimaryAnalystList = new SelectList(AnalystList, "Id", "FullName");
                ViewBag.CompanyStatusList = new SelectList( LovRepository.GetAll("CompanyStatus"), "Value", "Label");
                ViewBag.ActualEstimateList = new SelectList( LovRepository.GetAll("Actuals"), "Value", "Label");
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
        public ActionResult Add(CompaniesViewModel viewModel)
        {
            try
            {
                TempData["Actionname"] = "Add";
                ViewBag.countryList = new SelectList(LovRepository.GetAll("countries", "Clients").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.SectorsList = new SelectList(SectorsRepository.GetAll(Status: "1"), "Id", "Name");
                List<UserViewModel> AnalystList = UsersRepository.GetAnalysts();
                ViewBag.AnalystList = AnalystList.Select(x => new UserViewModel() { Id = x.Id, FullName = x.FullName }).ToList();
                ViewBag.PrimaryAnalystList = new SelectList(AnalystList, "Id", "FullName");
                ViewBag.SecondaryAnalystList = (AnalystList).Where(x => (viewModel.SecondaryAnalystIds != null && viewModel.SecondaryAnalystIds.Contains(x.Id))).ToList();
                ViewBag.CompanyStatusList = new SelectList(LovRepository.GetAll("CompanyStatus"), "Value", "Label");
                ViewBag.ActualEstimateList = new SelectList(LovRepository.GetAll("Actuals"), "Value", "Label");
                ViewBag.Uploaders = new SelectList(CompaniesRepository.GetUploaders(), "Id", "Name");
                ViewBag.ReportCode = new SelectList(CompaniesRepository.GetReportCodes(), "Id", "Name");
                CompaniesRepository.Add(viewModel, "Companies/Add");
                if (CommonRepository.IsError)
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

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            try
            {
                TempData["Actionname"] = "Edit";
                ViewBag.countryList = new SelectList(LovRepository.GetAll("countries", "Clients").OrderBy(x => x.Label), "Value", "Label");
                if (TempData["Errors"] != null) { ViewBag.Errors = TempData["Errors"]; }
                var response = CompaniesRepository.Get(Id);
                ViewBag.SectorsList = new SelectList(SectorsRepository.GetAll(Status: "1"), "Id", "Name");
                ViewBag.CompanyStatusList = new SelectList(LovRepository.GetAll("CompanyStatus"), "Value", "Label");
                ViewBag.ActualEstimateList = new SelectList(LovRepository.GetAll("Actuals"), "Value", "Label");
                ViewBag.PrimaryAnalystList = new SelectList(UsersRepository.GetAnalysts(), "Id", "FullName");
                List<UserViewModel> AnalystList = UsersRepository.GetAnalysts();
                ViewBag.AnalystList = AnalystList.Select(x => new UserViewModel() { Id = x.Id, FullName = x.FullName }).ToList();
                ViewBag.SecondaryAnalystList = (AnalystList).Where(x => (response.SecondaryAnalystIds != null && response.SecondaryAnalystIds.Contains(x.Id)));
                ViewBag.Uploaders = new SelectList(CompaniesRepository.GetUploaders(), "Id", "Name");
                ViewBag.ReportCode = new SelectList(CompaniesRepository.GetReportCodes(), "Id", "Name");
                ViewBag.UploadData = CompaniesRepository.GetUploadData(response.Code);
                return PartialView("Add", response);
            }
            catch (Exception e)
            {
                return View("Error", e); 
            }

        }
        [HttpPost]
        public ActionResult Edit(CompaniesViewModel viewModel)
        {
            try
            {
                TempData["Actionname"] = "Edit";
                ViewBag.countryList = new SelectList(LovRepository.GetAll("countries", "Clients").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.SectorsList = new SelectList(SectorsRepository.GetAll(Status: "1"), "Id", "Name");
                ViewBag.CompanyStatusList = new SelectList(LovRepository.GetAll("CompanyStatus"), "Value", "Label");
                ViewBag.ActualEstimateList = new SelectList(LovRepository.GetAll("Actuals"), "Value", "Label");
                List<UserViewModel> AnalystList = UsersRepository.GetAnalysts();
                ViewBag.AnalystList = AnalystList.Select(x => new UserViewModel() { Id = x.Id, FullName = x.FullName }).ToList();
                ViewBag.PrimaryAnalystList = new SelectList(AnalystList, "Id", "FullName");
                ViewBag.SecondaryAnalystList = (AnalystList).Where(x => (viewModel.SecondaryAnalystIds != null && viewModel.SecondaryAnalystIds.Contains(x.Id)));
                ViewBag.UploadData = CompaniesRepository.GetUploadData(viewModel.Code);
                ViewBag.Uploaders = new SelectList(CompaniesRepository.GetUploaders(), "Id", "Name");
                ViewBag.ReportCode = new SelectList(CompaniesRepository.GetReportCodes(), "Id", "Name");
                CompaniesRepository.Add(viewModel, "Companies/Edit");
                if (CommonRepository.IsError)
                {
                    ViewBag.Errors = CommonRepository.ResponseErrors;
                    return PartialView("Add", viewModel);
                }
                TempData["message"] = "Success";
                return RedirectToAction("Edit", new { viewModel.Id });
            }
            catch (Exception e)
            {
                return View("Error", e);
            }

        }
        [HttpGet]
        public ActionResult DeleteOrRestore(int Id, string Mode)
        {
            try
            {
                string Path = null;
                if (Mode == "Delete")
                { Path = "Companies/Delete"; }
                else Path = "Companies/Restore";
                CompaniesRepository.DeleteOrRestore(Id, Path);
                if (CommonRepository.IsError)
                {
                    ViewBag.Errors = CommonRepository.ResponseErrors;
                }
                TempData["message"] = CommonRepository.StatusMessage;
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View("Error", e);
            }

        }

        public ActionResult DeleteCompanyData(int CompanyId, long? CompanyDataId)
        {
            try
            {
                CompaniesRepository.DeleteCompanyData(CompanyDataId, "Companies/DeleteCompanyData");
                if (CommonRepository.IsError)
                {
                    TempData["Errors"] = CommonRepository.ResponseErrors;
               
                }
                TempData["message"] = CommonRepository.StatusMessage;
                return RedirectToAction("Edit", new { Id = CompanyId });
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }
        
        [HttpGet]
        public JsonResult GetAll(string Status="1")
        {
            var data = CompaniesRepository.GetAll(Status, null);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetSectorAnalystById(long SectorId)
        {
            var data = CompaniesRepository.GetSectorAnalystById(SectorId);
            var PrimaryAnalyst = data!=null?data.Where(x => x.AnalystType == "P"):data;
            var SecondaryAnalyst = data != null ?data.Where(x => x.AnalystType == "S"):data;
            return Json(new { PrimaryAnalyst, SecondaryAnalyst }, JsonRequestBehavior.AllowGet);
        }
    }
}
