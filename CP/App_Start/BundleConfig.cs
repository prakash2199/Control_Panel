using System.Web;
using System.Web.Optimization;

namespace CP
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
               "~/Scripts/jquery-2.1.1.min.js",
               "~/Scripts/jquery.datatable.min.js",
               "~/Scripts/datatables.buttons.min.js",
               "~/Scripts/jszip.min.js",
               "~/Scripts/buttons.html5.min.js",
               "~/Scripts/buttons.print.min.js",
               "~/Scripts/bootstrap.min.js",
               "~/Scripts/App.js",
               "~/Plugins/chosen/chosen.jquery.min.js",
               "~/scripts/bootstrap-datepicker.min.js",
                "~/Scripts/intlTelInput.js",
                "~/Scripts/Colorpicker.js",
                "~/Scripts/bootstrap-table.min.js",
                 "~/Scripts/bootstrap-table-export.min.js",
                 "~/Scripts/tableExport.min.js",
                 "~/Script/bootstrap-table-en-US.min.js",
                 "~/Script/jspdf.min.js", 
                 "~/Script/jspdf.plugin.autotable.js",
                 "~/Script/bootstrap-table-print.min.js"
               ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
               "~/Content/font-awesome.min.css",
               "~/Content/summernote.css",
               "~/Content/bootstrap-theme.min.css",
               "~/Content/bootstrap.min.css",
               "~/Content/selectize.css",
               "~/Content/Site.css",
               "~/Content/popup.css",
                "~/Content/colorpicker.css",
                "~/Content/datepicker.css",
                "~/Content/bootstrap-timepicker.min.css",
                "~/Plugins/chosen/chosen.min.css",
                "~/Content/jquery.datatables.min.css",
                "~/Content/buttons.datatables.min.css",
                "~/Content/bootstrap-table.min.css"
                ));
        }
    }
}
