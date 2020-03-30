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
    [Authorize]
    public class CorporateContactsController : Controller
    {
        // GET: CorporateContacts
        public ActionResult Index(string Status = "1")
        {
            ViewBag.Search = Status;
            return View();
        }

        public ActionResult Add()
        {
            try
            {
                TempData["Actionname"] = "Add";
                CorporateContactViewModel viewModel = new CorporateContactViewModel();
                ViewBag.CompanyList = new SelectList(CompaniesRepository.GetAll("1", null), "Id", "Name");
                ViewBag.Errors = null;
                return PartialView(viewModel);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }
        [HttpPost]
        public ActionResult Add(CorporateContactViewModel viewModel)
        {
            try
            {
                TempData["Actionname"] = "Add";
                ViewBag.CompanyList = new SelectList(CompaniesRepository.GetAll("1", null), "Id", "Name");

                CorporateContactsRepository.Add(viewModel, "CorporateContacts/Add");
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
                var response = CorporateContactsRepository.Get(Id);
                ViewBag.CompanyList = new SelectList(CompaniesRepository.GetAll("1", null), "Id", "Name");
                ViewBag.Errors = null;
                return PartialView("Add", response);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }

        }

        [HttpPost]
        public ActionResult Edit(CorporateContactViewModel viewModel)
        {
            try
            {
                TempData["Actionname"] = "Edit";
                ViewBag.CompanyList = new SelectList(CompaniesRepository.GetAll("1", null), "Id", "Name");
                CorporateContactsRepository.Add(viewModel, "CorporateContacts/Edit");
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
                { Path = "CorporateContacts/Delete"; }
                else Path = "CorporateContacts/Restore";
                CorporateContactsRepository.DeleteOrRestore(Id, Path);
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
            var data = CorporateContactsRepository.GetAll(Status);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


    }
}