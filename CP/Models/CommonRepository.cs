using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using EquiModels;
using Newtonsoft.Json;
using RestSharp;

namespace CP.Models
{
    public class CommonRepository
    {
        public static bool IsError { get; set; }
        public static string StatusMessage { get; set; }
        public static Dictionary<string, string> ResponseErrors { get; set; }

        public static string SessionId
        {
            get
            {
                return GetUser().SessionId;
            }
            
        }

        public static string GetWebApiConnectionString()
        {
            return ConfigurationManager.AppSettings["ApiUrl"].ToString();
        }

        public static List<T> GetListResponse<T>(string Path,Dictionary<string,string> list)
        {
            string ApiUrl = CommonRepository.GetWebApiConnectionString();
            var client = new RestClient(ApiUrl);
            var request = new RestRequest(Path, Method.GET);
           foreach(var res in list)
            {
                request.AddQueryParameter(res.Key, res.Value);
            }
            IRestResponse response = client.Execute(request);
            var data=JsonConvert.DeserializeObject<ListResponse<T>>(response.Content);

            if (data.IsError == true && (data.ResponseErrors == null || data.ResponseErrors.Count == 0)) throw new Exception(data.ErrorMessage);
            IsError = (data.IsError == true && data.ResponseErrors != null && data.ResponseErrors.Count > 0);
            StatusMessage = data.StatusMessage;
            ResponseErrors = data.ResponseErrors;
            return data.Model;
        }

        public static T GetResponse<T>(string Path, Dictionary<string, string> list)
        {
            string ApiUrl = CommonRepository.GetWebApiConnectionString();
            var client = new RestClient(ApiUrl);
            var request = new RestRequest(Path, Method.GET);
            foreach (var res in list)
            {
                request.AddQueryParameter(res.Key, res.Value);
            }
            IRestResponse response = client.Execute(request);
            var data = JsonConvert.DeserializeObject<SingleResponse<T>>(response.Content);
            if (data.IsError == true && (data.ResponseErrors == null || data.ResponseErrors.Count == 0)) throw new Exception(data.ErrorMessage);
            IsError = (data.IsError == true && data.ResponseErrors != null && data.ResponseErrors.Count > 0);
            StatusMessage = data.StatusMessage;
            ResponseErrors = data.ResponseErrors;
            return data.Model;
        }

        public static int SendRequest(String Path,string SessionId,object obj)
        {
            string ApiUrl = CommonRepository.GetWebApiConnectionString();
            var client = new RestClient(ApiUrl);
            var request = new RestRequest(Path, Method.POST);
            request.AddQueryParameter("SessionId", SessionId);
            request.AddJsonBody(obj);
            request.Timeout = 200000;
            IRestResponse response = client.Execute(request);
            var data = JsonConvert.DeserializeObject<SingleResponse<int>>(response.Content);
            if (data.IsError == true && (data.ResponseErrors == null || data.ResponseErrors.Count == 0)) throw new Exception(data.ErrorMessage);
            IsError = (data.IsError == true && data.ResponseErrors != null && data.ResponseErrors.Count > 0) ;
            StatusMessage = data.StatusMessage;
            ResponseErrors = data.ResponseErrors;
            return data.Model;
        }

        public static string SendMailRequest(String Path, string SessionId, object obj)
        {
            string ApiUrl = CommonRepository.GetWebApiConnectionString();
            var client = new RestClient(ApiUrl);
            var request = new RestRequest(Path, Method.POST);
            request.AddQueryParameter("SessionId", SessionId);
            request.AddJsonBody(obj);
            IRestResponse response = client.Execute(request);
            var data = JsonConvert.DeserializeObject<SingleResponse<string>>(response.Content);
            if (data.IsError == true && (data.ResponseErrors == null || data.ResponseErrors.Count == 0)) throw new Exception(data.ErrorMessage);
            IsError = (data.IsError == true && data.ResponseErrors != null && data.ResponseErrors.Count > 0);
            StatusMessage = data.StatusMessage;
            ResponseErrors = data.ResponseErrors;
            return data.Model;
        }

        public static UserViewModel GetUser()
        {
            return HttpContext.Current.Session["User"] as UserViewModel;
        }

        public static string GetDistributionTemplateData(string ReportBody,int ReportId)
        {
            string PreviewBody;
            string BodyText= @"<html><body><div align='center'><table width='700' cellpadding=0 cellpadding=0><tr><td><img width='250' height='52' src='cid:CentrumLogo'/></td></tr><tr><td><table width='100%' cellpadding='5'><tr><td align='left' style='padding:5px;'><font face='Frutiger LT 55 Roman' color='#808080' size='2' style='font-size:9.5pt;'>{1}</font></td><td align='right' style='padding:5px;'><font face='Frutiger LT 55 Roman' color='#808080' size='2' style='font-size:9.5pt;'>INVESTMENT RESEARCH DEPARTMENT</font></td></tr></table></td></tr><tr><td bgcolor='#f0f0f0'><table width='100%' cellpadding='10'><tr><td align='left' style='padding:10px;'><font face='Frutiger LT 55 Roman' size='2' style='font-size:{4}pt;'>{0}</font></td></tr></table></td></tr><tr><td bgcolor='#DADADA'><table width='100%' cellpadding='10'><tr><td width='300' valign='top' align='left' style='padding:10px;'><font face='Frutiger LT 55 Roman' size='1' style='font-size:9.5pt;text-align:justify'>CONFIDENTIALITY NOTICE<br /><P style='text-align:justify'>Please note that the information contained in this e-mail and its attachments (“the information”) is private and confidential, may be privileged, and is intended only for the use of the individual or entity to which it is addressed. Access to the information by anyone else is unauthorised.</P><p>If you are not the intended recipient; <br />(1) You are requested to inform us immediately by return e-mail and to irretrievably erase all copies of the information from your computer systems, <br />(2) You should not disclose the information to any other person, <br />(3) Any dissemination, distribution or copying of the information is strictly prohibited and, <br />(4) You should not take or refrain to take any action in reliance upon the information. All attachments to this message have been scanned to remove viruses; however, we accept no responsibility for these attachments once they have left our office networking environment.</font></td></tr></table><tr><td><br /><table width='100%'><tr><td align='left'><font face='Frutiger LT 55 Roman' size='1' style='font-size:9.5pt;'><font color='#000066'><b>Equitec Software</b></font> 4B-44 Paragon Plaza,Pheonix marketcity,Kurla west, India <br />T: +91 22 6180 2025 <a href='http://www.equitec.in'><font color='#000000'><b>www.equitec.in</b></font></a></font></td><td align='right'><img src='cid:CentrumLogo' alt='centrum_icon logo' width='150' height='30' /></td></tr></table></td></tr></table></div></body></html>";
            PreviewBody = BodyText.Replace("{0}",ReportBody);
            PreviewBody = PreviewBody.Replace("{1}", DateTime.Now.ToString("MMMM dd, yyyy").ToUpper());
            PreviewBody = PreviewBody.Replace("<p>&nbsp;</p>", "");
            PreviewBody = PreviewBody.Replace("<p>", @"<p align=""justify"">");
            PreviewBody = PreviewBody.Replace(@"<p class=""MsoNormal"">", @"<p style=""margin:0;"">");
            PreviewBody = PreviewBody.Replace("<br><div>", "</div><br /><div>");
            PreviewBody = PreviewBody.Replace("<br></div>", "</div><br />");
            return PreviewBody;
        }

    


    }
}
