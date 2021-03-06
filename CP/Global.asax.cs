﻿using CP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace CP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
          //var response=  AuthenticationRepository.ValidateLicense();
          //  if (response == null) { return Cont()}
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);



        }

        protected void Session_Start(object sender, EventArgs e)
        {

             //string ApiKey =  AuthenticationRepository. 

            if (HttpContext.Current.Request.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
                HttpContext.Current.Response.End();
            }

        }




    }
}
