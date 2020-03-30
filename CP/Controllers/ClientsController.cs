using CP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EquiModels;
using System.IO;
using System.Configuration;

namespace CP.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ClientsController : Controller
    {
        // GET: Clients
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
        public ActionResult Add()
        {
            try
            {
                TempData["Actionname"] = "Add";
                ClientViewModel viewModel = new ClientViewModel();
                ViewBag.countryList = new SelectList(LovRepository.GetAll( "countries","Clients").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.TierList = new SelectList(LovRepository.GetAll("Tiers","Clients").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.PrimarySalePersonList = new SelectList(ClientsRepository.GetAllSalesPerson().OrderBy(x => x.FirstName), "Id", "Firstname");
                ViewBag.SecondarySalePersonList = new SelectList(ClientsRepository.GetAllSalesPerson().OrderBy(x => x.FirstName), "Id", "Firstname");
                ViewBag.ClientTypeList = new SelectList(LovRepository.GetAll("ClientTypes", "Clients").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.Errors = null;
                return PartialView(viewModel);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }
        [HttpPost]
        public ActionResult Add(ClientViewModel viewModel)
        {
            try
            {
                TempData["Actionname"] = "Add";
                ViewBag.countryList = new SelectList(LovRepository.GetAll("countries", "Clients").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.TierList = new SelectList(LovRepository.GetAll("Tiers", "Clients").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.PrimarySalePersonList = new SelectList(ClientsRepository.GetAllSalesPerson().OrderBy(x => x.FirstName), "Id", "Firstname");
                ViewBag.SecondarySalePersonList = new SelectList(ClientsRepository.GetAllSalesPerson().OrderBy(x => x.FirstName), "Id", "Firstname");
                ViewBag.ClientTypeList = new SelectList(LovRepository.GetAll("ClientTypes", "Clients").OrderBy(x => x.Label), "Value", "Label");
                if (viewModel.SecondarySalesPeopleId != null)
                {
                    ViewBag.MarkToSalesPeople = string.Join(",", viewModel.SecondarySalesPeopleId);
                }
                ClientsRepository.Add(viewModel, "Clients/Add");
                if (CommonRepository.IsError)
                {
                    ViewBag.Errors = CommonRepository.ResponseErrors;
                    return PartialView(viewModel);
                }
                else
                {
                    TempData["message"] = CommonRepository.StatusMessage;
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
              
                var response = ClientsRepository.Get(Id);
                ViewBag.countryList = new SelectList(LovRepository.GetAll("countries", "Clients").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.TierList = new SelectList(LovRepository.GetAll("Tiers", "Clients").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.PrimarySalePersonList = new SelectList(ClientsRepository.GetAllSalesPerson().OrderBy(x => x.FirstName), "Id", "Firstname");
                ViewBag.SecondarySalePersonList = new SelectList(ClientsRepository.GetAllSalesPerson().OrderBy(x => x.FirstName), "Id", "Firstname");
                ViewBag.ClientTypeList = new SelectList(LovRepository.GetAll("ClientTypes", "Clients").OrderBy(x => x.Label), "Value", "Label");
                if (response.SecondarySalesPeopleId != null)
                {
                    ViewBag.MarkToSalesPeople = string.Join(",", response.SecondarySalesPeopleId);
                }
                ViewBag.Errors = null;
                return PartialView("Add", response);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }

        }
        [HttpPost]
        public ActionResult Edit(ClientViewModel viewModel)
        {
            try
            {
                TempData["Actionname"] = "Edit";
                ViewBag.countryList = new SelectList(LovRepository.GetAll("countries", "Clients").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.TierList = new SelectList(LovRepository.GetAll("Tiers", "Clients").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.PrimarySalePersonList = new SelectList(ClientsRepository.GetAllSalesPerson().OrderBy(x => x.FirstName), "Id", "Firstname");
                ViewBag.SecondarySalePersonList = new SelectList(ClientsRepository.GetAllSalesPerson().OrderBy(x => x.FirstName), "Id", "Firstname");
                ViewBag.ClientTypeList = new SelectList(LovRepository.GetAll("ClientTypes", "Clients").OrderBy(x => x.Label), "Value", "Label");
                if (viewModel.SecondarySalesPeopleId != null)
                {
                    ViewBag.MarkToSalesPeople = string.Join(",", viewModel.SecondarySalesPeopleId);
                }
                ClientsRepository.Add(viewModel, "Clients/Edit");
                if (CommonRepository.IsError)
                {
                    ViewBag.Errors = CommonRepository.ResponseErrors;
                    return PartialView("Add", viewModel);
                }
                TempData["message"] = CommonRepository.StatusMessage;
                return RedirectToAction("Edit", new { viewModel.Id });
            }
            catch (Exception e)
            {
                return View("Error", e);
            }

        }
        public ActionResult DeleteOrRestore(int Id, string Mode)
        {
            try
            {
                string Path = null;
                if (Mode == "Delete")
                { Path = "Clients/Delete"; }
                else Path = "Clients/Restore";
                ClientsRepository.DeleteOrRestore(Id, Path);
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
        [HttpGet]
        public JsonResult GetAll(string Status)
        {
            var data = ClientsRepository.GetAll(Status);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}