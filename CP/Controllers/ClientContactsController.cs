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
    public class ClientContactsController : Controller
    {
        // GET: ClientContacts
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
            TempData["Actionname"] = "Add";
            ClientContactViewModel viewModel = new ClientContactViewModel();
            ViewBag.ClientList = new SelectList(ClientsRepository.GetAll("1").OrderBy(x => x.Name), "Id", "Name");
           ViewBag.AnalystList = new SelectList(UsersRepository.GetAnalysts(), "Id", "Firstname");
            ViewBag.ProductsList = new SelectList(LovRepository.GetAll("Website","Products").OrderBy(x => x.Label), "Value", "Label");
            ViewBag.SectorsList = new SelectList(SectorsRepository.GetAll("1"), "Id", "Name");
            ViewBag.CompanyList= new SelectList(CompaniesRepository.GetAll("1",null), "Id", "Name");
            ViewBag.PriorityList = new SelectList(LovRepository.GetAll("Priority", "Client Contacts"), "Value", "Label");
            if (viewModel.RemarksList != null)
            {
                List<string> Remark = viewModel.RemarksList.Select(x => x.RemarkDes).ToList();
                ViewBag.MarkRemark = Remark;
            }

            return PartialView(viewModel);
        }
        [HttpPost]
        public ActionResult Add(ClientContactViewModel viewModel)
        {
            TempData["Actionname"] = "Add";
            viewModel.UserId = CommonRepository.GetUser().Id;
            ViewBag.ClientList = new SelectList(ClientsRepository.GetAll("1").OrderBy(x => x.Name), "Id", "Name");
            ViewBag.AnalystList = new SelectList(UsersRepository.GetAnalysts(), "Id", "Firstname");
            ViewBag.ProductsList = new SelectList(LovRepository.GetAll("Website", "Products").OrderBy(x => x.Label), "Value", "Label");
            ViewBag.SectorsList = new SelectList(SectorsRepository.GetAll("1"), "Id", "Name");
            ViewBag.CompanyList = new SelectList(CompaniesRepository.GetAll("1", null), "Id", "Name");
            ViewBag.PriorityList = new SelectList(LovRepository.GetAll("Priority", "Client Contacts"), "Value", "Label");

            if (viewModel.RemarksList != null)
            {
                List<string> Remark = viewModel.RemarksList.Where(x => x.Status == false).Select(x => x.RemarkDes).ToList();
                ViewBag.MarkRemark = Remark;
                viewModel.RemarksList = viewModel.RemarksList.Where(x => x.Status == false).ToList();
            }
            if (viewModel.PreferredAnalysts != null)
            {
               ViewBag.MarkToAnalyst = string.Join(",", viewModel.PreferredAnalysts);
            }
            if (viewModel.PreferredProducts != null)
            {
                ViewBag.MarkToPreferredProducts = string.Join(",", viewModel.PreferredProducts);
            }
            if (viewModel.Sectors != null)
            {
                ViewBag.MarkToSectors = string.Join(",", viewModel.Sectors);
            }
            if (viewModel.Companies != null)
            {
                ViewBag.MarkToCompanies = string.Join(",", viewModel.Companies);
            }

            ClientContactsRepository.Add(viewModel, "ClientContacts/Add");
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

        public ActionResult Edit(int Id)
        {
            try
            {
                TempData["Actionname"] = "Edit";
                var response = ClientContactsRepository.Get(Id);
                ViewBag.ClientList = new SelectList(ClientsRepository.GetAll("1").OrderBy(x => x.Name), "Id", "Name");
                ViewBag.AnalystList = new SelectList(UsersRepository.GetAnalysts(), "Id", "Firstname");
                ViewBag.ProductsList = new SelectList(LovRepository.GetAll("Website", "Products").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.SectorsList = new SelectList(SectorsRepository.GetAll("1"), "Id", "Name");
                ViewBag.CompanyList = new SelectList(CompaniesRepository.GetAll("1", null), "Id", "Name");
                ViewBag.PriorityList = new SelectList(LovRepository.GetAll("Priority", "Client Contacts"), "Value", "Label");

                if (response.Remarks != null)
                { ViewBag.MarkRemark = response.Remarks; }

                if (response.PreferredAnalysts != null)
                { ViewBag.MarkToAnalyst = string.Join(",", response.PreferredAnalysts); }

                if (response.Sectors != null)
                { ViewBag.MarkToSectors = string.Join(",", response.Sectors); }

                if (response.PreferredProducts != null)
                { ViewBag.MarkToPreferredProducts = string.Join(",", response.PreferredProducts); }

                if (response.Companies != null)
                { ViewBag.MarkToCompanies = string.Join(",", response.Companies); }
                ViewBag.Errors = null;
                return PartialView("Add", response);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }

        }


        [HttpPost]
        public ActionResult Edit(ClientContactViewModel viewModel)
        {
            try
            {
                TempData["Actionname"] = "Edit";
                viewModel.UserId = CommonRepository.GetUser().Id;
                ViewBag.ClientList = new SelectList(ClientsRepository.GetAll("1").OrderBy(x => x.Name), "Id", "Name");
                ViewBag.AnalystList = new SelectList(UsersRepository.GetAnalysts(), "Id", "Firstname");
                ViewBag.ProductsList = new SelectList(LovRepository.GetAll("Website", "Products").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.SectorsList = new SelectList(SectorsRepository.GetAll("1"), "Id", "Name");
                ViewBag.CompanyList = new SelectList(CompaniesRepository.GetAll("1", null), "Id", "Name");
                ViewBag.PriorityList = new SelectList(LovRepository.GetAll("Priority", "Client Contacts"), "Value", "Label");
                if (viewModel.RemarksList != null)
                {
                    List<string> Remark = viewModel.RemarksList.Where(x => x.Status == false).Select(x => x.RemarkDes).ToList();
                    ViewBag.MarkRemark = Remark;
                    viewModel.RemarksList = viewModel.RemarksList.Where(x => x.Status == false).ToList();
                }
                if (viewModel.PreferredAnalysts != null)
                {
                    ViewBag.MarkToAnalyst = string.Join(",", viewModel.PreferredAnalysts);
                }
                if (viewModel.PreferredProducts != null)
                {
                    ViewBag.MarkToPreferredProducts = string.Join(",", viewModel.PreferredProducts);
                }
                if (viewModel.Sectors != null)
                {
                    ViewBag.MarkToSectors = string.Join(",", viewModel.Sectors);
                }
                if (viewModel.Companies != null)
                {
                    ViewBag.MarkToCompanies = string.Join(",", viewModel.Companies);
                }
                ClientContactsRepository.Add(viewModel, "ClientContacts/Edit");
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
                { Path = "ClientContacts/Delete"; }
                else Path = "ClientContacts/Restore";
                ClientContactsRepository.DeleteOrRestore(Id, Path);
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
            var data = ClientContactsRepository.GetAll(Status);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}