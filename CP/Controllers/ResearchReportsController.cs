using CP.Models;
using EquiModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace CP.Controllers
{
    [Authorize]
    public class ResearchReportsController : Controller
    {
        // GET: ResearchReports
        public ActionResult Index(string Status = "1")
        {
            try
            {
                ViewBag.Search = Status;
                ResearchReportViewModel viewModel = new ResearchReportViewModel
                {
                    Status = Convert.ToInt32(Status)
                };
                return View(viewModel);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }

        [HttpGet]
        public JsonResult GetAll(string Status)
        {
            var data = ResearchReportRepository.GetAll(Status);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            try
            {
                TempData["Actionname"] = "Add";
                ResearchReportViewModel viewModel = new ResearchReportViewModel
                {
                    ReportDate = DateTime.Now
                };
                ViewBag.ProductsList = new SelectList(LovRepository.GetAll("Website", "Products").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.AnalystList = new SelectList(UsersRepository.GetAnalysts(), "Id", "FullName");
                ViewBag.SectorsList = new SelectList(SectorsRepository.GetAll("1"), "Id", "Name");
                ViewBag.CompanyList = new SelectList(CompaniesRepository.GetAll("1", null), "Id", "Name");
                ViewBag.RatingList = new SelectList(LovRepository.GetAll("Ratings" , "Research Reports").OrderBy(x => x.Label), "Value", "Label");
                return PartialView(viewModel);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }

        [HttpPost]
        public ActionResult Add(ResearchReportViewModel viewModel,FormCollection formCollection,HttpPostedFileBase PDF=null)
        {
            try
            {
                TempData["Actionname"] = "Add";
                if (viewModel.SectorList != null)
                {
                    ViewBag.MarkToSectors = string.Join(",", viewModel.SectorList);
                }
                if (viewModel.SecondaryAnalystId != null)
                {
                    ViewBag.MarkToAnalysts = string.Join(",", viewModel.SecondaryAnalystId);
                }
                ViewBag.ProductsList = new SelectList(LovRepository.GetAll("Website", "Products").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.AnalystList = new SelectList(UsersRepository.GetAnalysts(), "Id", "FullName");
                ViewBag.SectorsList = new SelectList(SectorsRepository.GetAll("1"), "Id", "Name");
                ViewBag.CompanyList = new SelectList(CompaniesRepository.GetAll("1", null), "Id", "Name");
                ViewBag.RatingList = new SelectList(LovRepository.GetAll("Ratings", "Research Reports").OrderBy(x => x.Label), "Value", "Label");
                KeyValuePair<string, string> kp = new KeyValuePair<string, string>();
                string fileName = null;
                string fullPath = null;
                if (PDF != null)
                {
                    fileName = Path.GetFileName(PDF.FileName);
                    fullPath = Server.MapPath("~") + @"\AnalystReports\PDF\" + fileName;
                }
                else
                {
                    fileName = formCollection["ReportName"];
                    fullPath = Server.MapPath("~") + @"\AnalystReports\PDF\" + fileName;
                }
                kp = UploadFilesRepository.Uploadfiles(fileName, fullPath);
                viewModel.PDF = kp.Key;
                viewModel.RepDate = Convert.ToString(viewModel.ReportDate);
                ResearchReportRepository.Add(viewModel, "ResearchReports/Add");
                if (CommonRepository.IsError)
                {
                    ViewBag.Errors = CommonRepository.ResponseErrors;
                    ViewBag.CompanyDetails = viewModel.CompaniesList;
                    return PartialView(viewModel);
                }
                else
                {
                    if(PDF != null)
                    {
                        PDF.SaveAs(kp.Value);
                    }
                    TempData["message"] = CommonRepository.StatusMessage;
                    return RedirectToAction("Add");
                }
            }
            catch(Exception ex)
            {
                return View("Error", ex);
            }
        }

        public ActionResult Edit(int Id)
        {
            try
            {
                TempData["Actionname"] = "Edit";
                var response = ResearchReportRepository.Get(Id);
                if (response.SectorList != null)
                {
                    ViewBag.MarkToSectors = string.Join(",", response.SectorList);
                }
                if (response.SecondaryAnalystId != null)
                {
                    ViewBag.MarkToAnalysts = string.Join(",", response.SecondaryAnalystId);
                }
                ViewBag.CompanyDetails = response.CompaniesList;
                ViewBag.ProductsList = new SelectList(LovRepository.GetAll("Website", "Products").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.AnalystList = new SelectList(UsersRepository.GetAnalysts(), "Id", "FullName");
                ViewBag.SectorsList = new SelectList(SectorsRepository.GetAll("1"), "Id", "Name");
                ViewBag.CompanyList = new SelectList(CompaniesRepository.GetAll("1", null), "Id", "Name");
                ViewBag.RatingList = new SelectList(LovRepository.GetAll("Ratings", "Research Reports").OrderBy(x => x.Label), "Value", "Label");
                return PartialView("Add", response);
            }
            catch (Exception e)
            {
                return View("Error", e);
            }
        }

        [HttpPost]
        public ActionResult Edit(ResearchReportViewModel viewModel, FormCollection formCollection, HttpPostedFileBase PDF)
        {
            try
            {
                TempData["Actionname"] = "Edit";
                if (viewModel.SectorList != null)
                {
                    ViewBag.MarkToSectors = string.Join(",", viewModel.SectorList);
                }
                if (viewModel.SecondaryAnalystId != null)
                {
                    ViewBag.MarkToAnalysts = string.Join(",", viewModel.SecondaryAnalystId);
                }
                ViewBag.ProductsList = new SelectList(LovRepository.GetAll("Website", "Products").OrderBy(x => x.Label), "Value", "Label");
                ViewBag.AnalystList = new SelectList(UsersRepository.GetAnalysts(), "Id", "FullName");
                ViewBag.SectorsList = new SelectList(SectorsRepository.GetAll("1"), "Id", "Name");
                ViewBag.CompanyList = new SelectList(CompaniesRepository.GetAll("1", null), "Id", "Name");
                ViewBag.RatingList = new SelectList(LovRepository.GetAll("Ratings", "Research Reports").OrderBy(x => x.Label), "Value", "Label");
                string ReportName= ResearchReportRepository.Get(viewModel.Id).PDF;
                KeyValuePair<string, string> Kp = new KeyValuePair<string, string>();
                string fileName = null;
                string fullPath = null;
                if (PDF != null)
                {
                    ResearchReportRepository.DeleteReportFile(Server.MapPath("~") + @"\AnalystReports\PDF\", ReportName);
                    fileName = Path.GetFileName(PDF.FileName);
                    fullPath = Server.MapPath("~") + @"\AnalystReports\PDF\" + fileName;
                }
                else
                {
                    fileName = formCollection["ReportName"];
                    fullPath = Server.MapPath("~") + @"\AnalystReports\PDF\" + fileName;
                }
                Kp = UploadFilesRepository.Uploadfiles(fileName, fullPath);
                viewModel.PDF = Kp.Key;
                viewModel.RepDate = Convert.ToString(viewModel.ReportDate);
                ResearchReportRepository.Edit(viewModel, "ResearchReports/Edit");
                if (CommonRepository.IsError)
                {
                    ViewBag.Errors = CommonRepository.ResponseErrors;
                    var comp = viewModel.CompaniesList;
                    ViewBag.CompanyDetails = comp;
                    return PartialView("Add",viewModel);
                }
                else
                {
                    if(PDF != null)
                    {
                        PDF.SaveAs(Kp.Value);
                    }
                    TempData["message"] = CommonRepository.StatusMessage;
                    return RedirectToAction("Edit",new { viewModel.Id });
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpGet]
        public ActionResult DeleteOrRestore(int Id, string Mode)
        {
            try
            {
                string Path = null;
                if (Mode == "Delete")
                { Path = "ResearchReports/Delete"; }
                else Path = "ResearchReports/Restore";
                ResearchReportRepository.DeleteOrRestore(Id, Path);
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

        public JsonResult GetCompanyData(int CompanyId)
        {
            var data = ResearchReportRepository.GetCompanyData(Convert.ToString(CompanyId));
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Distribute(int Id)
        {
            try
            {
                TempData["Actionname"] = "Distribute";
                DistributionViewModel distributionViewModel = new DistributionViewModel();
                ViewBag.DistributionList = ResearchReportRepository.GetDistributionList(Convert.ToString(Id));
                var reportData = ResearchReportRepository.Get(Id);
                if (reportData != null)
                {
                    if (reportData.PDF != null)
                    {
                        distributionViewModel.FilePath = Server.MapPath("~") + @"\AnalystReports\PDF\" + reportData.PDF;
                    }
                    distributionViewModel.Subject = reportData.Category;
                    distributionViewModel.PDF = reportData.PDF;
                    distributionViewModel.ReportId = reportData.Id;
                    distributionViewModel.ReportBody = "Dear";
                }
                return PartialView(distributionViewModel);
            }
            catch(Exception ex)
            {
                return View("Error", ex);
            }
            
        }

        [HttpPost]
        public ActionResult Distribute(DistributionViewModel viewModel)
        {
            try
            {
                TempData["Actionname"] = "DistributeMassMail";
                ViewBag.DistributionList = ResearchReportRepository.GetDistributionList(Convert.ToString(viewModel.ReportId));
                return PartialView("DistributeMail", viewModel);
            }
            catch(Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DistributeMassMail(DistributionViewModel viewModel)
        {
            try
            {
                TempData["Actionname"] = "DistributeMailpreview";
                string BodyText = CommonRepository.GetDistributionTemplateData(viewModel.ReportBody, viewModel.ReportId);
                viewModel.ReportBody = BodyText;
                return PartialView("DistributeMailPreview", viewModel);
            }
            catch(Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DistributeMailpreview(DistributionViewModel viewModel, IEnumerable<DistributionList> contactlst,FormCollection collection)
        {
            try
            {
                TempData["Actionname"] = "DistributeMailpreview";
                viewModel.ContactLists = new List<DistributionList>();
                string FromName = (string)ConfigurationManager.AppSettings["FromName"];
                string FromAddress = (string)ConfigurationManager.AppSettings["FromAddress"];
                var RBody = collection["RBody"];
                viewModel.ReportBody = RBody;
                if (viewModel.IsTestMail)
                {
                    if (viewModel.Emails != null)
                    {
                        viewModel.EmailIds = viewModel.Emails.Split(',').ToList();
                        foreach (var em in viewModel.EmailIds)
                        {
                            DistributionList distributionList = new DistributionList
                            {
                                Email = em,
                                FirstName = "Test",
                                LastName = "Test",
                                ClientName = "Test",
                                Name = "Test"
                            };
                            viewModel.ContactLists.Add(distributionList);
                        }
                    }
                }
                else
                {
                    viewModel.ContactLists = ResearchReportRepository.GetDistributionList(Convert.ToString(viewModel.ReportId));
                }
                List<MailAttachmentsViewModel> attachment = null;
                if (viewModel.FilePath != null)
                {
                    attachment = new List<MailAttachmentsViewModel>();
                    MailAttachmentsViewModel m = new MailAttachmentsViewModel
                    {
                        AttachmentType = 'T',
                        FilePath = viewModel.FilePath
                    };
                    attachment.Add(m);
                }
                MailingQueueViewModel mailingQueueViewModel = new MailingQueueViewModel
                {
                    Id = Convert.ToString(Guid.NewGuid()),
                    FromName = FromName,
                    FromEmail = FromAddress,
                    Subject = viewModel.Subject,
                    HtmlBody = viewModel.ReportBody,
                    To = viewModel.ContactLists,
                    SessionId = CommonRepository.SessionId,
                    Bcc = (string)ConfigurationManager.AppSettings["BCC"],
                    CC = (string)ConfigurationManager.AppSettings["CC"],
                    Attachments = attachment,
                    RetryAttempts = Convert.ToInt32(ConfigurationManager.AppSettings["RetryAttempts"]),
                    ReportId = viewModel.ReportId,
                    Active = true
                };
                ResearchReportRepository.SendBulkMail(mailingQueueViewModel, "ResearchReports/SendBulkMail");
                if (CommonRepository.IsError)
                {
                    ViewBag.Errors = CommonRepository.ResponseErrors;
                    return PartialView(viewModel);
                }
                else
                {
                    return RedirectToAction("MailProgress");
                }
            }
            catch(Exception ex)
            {
                return View("Error", ex);
            }
        }

        public ActionResult MailProgress()
        {
            try
            {
                return PartialView("MailProgress");
            }
            catch(Exception ex)
            {
                return View("Error", ex);
            }
        }
    }
}