using CP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EquiModels;
using System.Drawing;

namespace CP.Controllers
{
    [Authorize]
    public class FormatGroupController : Controller
    {
        // GET: FormatGroup
        [HttpGet]
        public ActionResult Index(string Status = "1")
        {
            try
            {
                ViewBag.FormatGroup = FormatGroupRepository.GetAll(Status);
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
                TempData["ActionName"] = "Add";
                FormatGroupViewModel model = new FormatGroupViewModel();
                return PartialView("_Add", model);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }
        [HttpPost]
        public ActionResult Add(FormatGroupViewModel model)
        {
            try
            {
                TempData["ActionName"] = "Add";
                if (model.FormatGroup == null) ModelState.AddModelError("FormatGroup", "Group required.");
                if (model.ForeColor == null || model.ForeColor.Trim().Length == 0) ModelState.AddModelError("ForeColor", "Fore color required.");
                if (model.BackColor == null || model.BackColor.Trim().Length == 0) ModelState.AddModelError("BackColor", "Back color required.");
                if (ModelState.IsValid)
                {
                    string forecolor = model.ForeColor;
                    if (forecolor.Length == 6) forecolor = "#" + forecolor;
                    string backcolor = model.BackColor;
                    if (backcolor.Length == 6) backcolor = "#" + backcolor;
                    model.ForeColor = Convert.ToString(ColorTranslator.ToWin32(ColorTranslator.FromHtml(forecolor)));
                    model.BackColor = Convert.ToString(ColorTranslator.ToWin32(ColorTranslator.FromHtml(backcolor)));
                    FormatGroupRepository.Add(model, "FormatGroup/Add");
                    TempData["message"] = CommonRepository.StatusMessage;
                    return PartialView("_Add", model);
                }
                else
                {                    
                  return PartialView("_Add", model);                  
                  
                }
               

            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }
        [HttpGet]
        public ActionResult Edit(long Id)
        {
            try
            {
                TempData["ActionName"] = "Edit";
                FormatGroupViewModel viewmodel = new FormatGroupViewModel();
                var Response = FormatGroupRepository.Get(Id);
                Response.ForeColor = System.Drawing.ColorTranslator.ToHtml(System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(Response.ForeColor)));
                Response.BackColor = System.Drawing.ColorTranslator.ToHtml(System.Drawing.ColorTranslator.FromWin32(Convert.ToInt32(Response.BackColor)));
                return PartialView("_Add", Response);
            }
            catch(Exception e)
            {
                return PartialView("Error", e);
            }
        }
        [HttpPost]
        public ActionResult Edit(FormatGroupViewModel model)
        {
            try
            {
                TempData["ActionName"] = "Edit";
                if (model.FormatGroup == null) ModelState.AddModelError("FormatGroup", "Group required.");
                if (model.ForeColor == null || model.ForeColor.Trim().Length == 0) ModelState.AddModelError("ForeColor", "Fore color required.");
                if (model.BackColor == null || model.BackColor.Trim().Length == 0) ModelState.AddModelError("BackColor", "Back color required.");
                if (ModelState.IsValid)
                {
                    string forecolor = model.ForeColor;
                    if (forecolor.Length == 6) forecolor = "#" + forecolor;
                    string backcolor = model.BackColor;
                    if (backcolor.Length == 6) backcolor = "#" + backcolor;
                    model.ForeColor = Convert.ToString(ColorTranslator.ToWin32(ColorTranslator.FromHtml(forecolor)));
                    model.BackColor = Convert.ToString(ColorTranslator.ToWin32(ColorTranslator.FromHtml(backcolor)));
                    FormatGroupRepository.Add(model, "FormatGroup/Edit");
                    TempData["message"] = CommonRepository.StatusMessage;
                    return PartialView("_Add", model);
                }
                else
                {
                    return PartialView("_Add", model);
                }
                
            }
            catch(Exception e)
            {
                return PartialView("Error", e);
            }

        }
        [HttpGet]
        public ActionResult DeleteFormatGroup(long Id,string Mode)
        {
            try
            {
                string Path = null;
                if (Mode == "Delete")
                { Path = "FormatGroup/Delete"; }
                else Path = "FormatGroup/Restore";
                FormatGroupRepository.DeleteOrRestore(Id, Path);
                if (CommonRepository.IsError)
                {
                    ViewBag.Errors = CommonRepository.ResponseErrors;
                }
                TempData["message"] = CommonRepository.StatusMessage;
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return PartialView("Error", e);
            }
        }
    }
}