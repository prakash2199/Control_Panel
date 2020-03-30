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
    public class UsersController : Controller
    {
        // GET: Users
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
                UserViewModel viewModel = new UserViewModel();
                ViewBag.RoleList = new SelectList(LovRepository.GetAll("Roles"), "Value", "Label");
                ViewBag.PrefixList = new SelectList(LovRepository.GetAll("Prefix"), "Value", "Label");
                ViewBag.Department = new SelectList(LovRepository.GetAll("Department"), "Value", "Label");
                ViewBag.Errors = null;
                viewModel.ImgPath = ConfigurationManager.AppSettings["ImgUrl"];
                return PartialView(viewModel);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }

        [HttpPost]
        public ActionResult Add(UserViewModel viewModel,HttpPostedFileBase file=null)
        {
            try
            {
                TempData["actionname"] = "Add";
                ViewBag.RoleList = new SelectList(LovRepository.GetAll("Roles"), "Value", "Label");
                ViewBag.PrefixList = new SelectList(LovRepository.GetAll("Prefix"), "Value", "Label");
                ViewBag.Department = new SelectList(LovRepository.GetAll("Department"), "Value", "Label");
                viewModel.ImgPath = ConfigurationManager.AppSettings["ImgUrl"];

                if (file != null)
                {
                    viewModel.Avatar = UsersRepository.GetNextUserId("Users/NextUserId");
                }
                UsersRepository.Add(viewModel, "Users/Add");
                if (CommonRepository.IsError)
                {
                    ViewBag.Errors = CommonRepository.ResponseErrors;
                    return PartialView(viewModel);
                }
                else
                {
                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath(viewModel.ImgPath), Path.GetFileName(viewModel.Avatar));
                        file.SaveAs(path);
                    }
                    TempData["message"] = "Success";
                    return RedirectToAction("Add");
                }

            }
            catch(Exception e)
            {
                return View("Error", e);
            }
        }

        public ActionResult Edit(int Id)
        {
            try
            {
                TempData["Actionname"] = "Edit";
                ViewBag.RoleList = new SelectList(LovRepository.GetAll("Roles"), "Value", "Label");
                ViewBag.PrefixList = new SelectList(LovRepository.GetAll("Prefix"), "Value", "Label");
                ViewBag.Department = new SelectList(LovRepository.GetAll("Department"), "Value", "Label");
                var response = UsersRepository.Get(Id);
                response.ImgPath = ConfigurationManager.AppSettings["ImgUrl"];

                return PartialView("Add", response);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel viewModel,HttpPostedFileBase file)
        {
            try
            {
                TempData["Actionname"] = "Edit";
                ViewBag.RoleList = new SelectList(LovRepository.GetAll("Roles"), "Value", "Label");
                ViewBag.PrefixList = new SelectList(LovRepository.GetAll("Prefix"), "Value", "Label");
                ViewBag.Department = new SelectList(LovRepository.GetAll("Department"), "Value", "Label");
                viewModel.ImgPath = ConfigurationManager.AppSettings["ImgUrl"];
                if (file != null)
                {
                    viewModel.Avatar = "UserImage_" + viewModel.Id +".jpg";
                }
                UsersRepository.Add(viewModel, "Users/Edit");
                if (CommonRepository.IsError)
                {
                    ViewBag.Errors = CommonRepository.ResponseErrors;
                    return PartialView("Add", viewModel);
                }
                else
                {
                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath(viewModel.ImgPath), Path.GetFileName(viewModel.Avatar));
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                        file.SaveAs(path);
                    }
                    TempData["message"] = "Success";
                    return RedirectToAction("Edit", new { Id = viewModel.Id });
                }
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
                { Path = "Users/Delete"; }
                else Path = "Users/Restore";
                UsersRepository.DeleteOrRestore(Id, Path);
                if (CommonRepository.IsError)
                {
                    ViewBag.Errors = CommonRepository.ResponseErrors;
                }
                TempData["message"] = "Success";
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
            var data =  UsersRepository.GetAll(Status);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
