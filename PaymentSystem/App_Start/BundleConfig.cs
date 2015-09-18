using System.Web;
using System.Web.Optimization;

namespace PaymentSystem
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.9.2.custom.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/backstretch").Include(
                                    "~/Scripts/jquery.backstretch.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/additionScripts").Include(
                    "~/Scripts/jquery.dcjqaccordion.2.7.js",
                    "~/Scripts/jquery.scrollTo.min.js",
                    "~/Scripts/jquery.nicescroll.js",
                    "~/Scripts/jquery.sparkline.js",
                    "~/Scripts/common-scripts.js",
                    "~/Scripts/gritter/js/jquery.gritter.js",
                    "~/Scripts/gritter-conf.js",
                    "~/Scripts/sparkline-chart.js",
                    "~/Scripts/zabuto_calendar.js",
                    "~/Scripts/chart-master/Chart.js",
                    "~/Scripts/bootstrap-table.js",
                    "~/Scripts/bootstrap-table-ru-RU.js",
                    "~/Scripts/sweetalert.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/chart").Include(
                    "~/Scripts/chart-master/Chart.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/font-awesome/css/font-awesome.css",
                      "~/Content/style.css",
                      "~/Content/style-reponsive.css",
                      "~/Content/table-responsive.css",
                      "~/Content/bootstrap-table.css",
                      "~/Content/sweetalert.css"));

            bundles.Add(new ScriptBundle("~/bundles/form").Include(
                    "~/Scripts/form-component.js",
                    "~/Scripts/bootstrap-switch.js",
                    "~/Scripts/jquery.tagsinput.js",
                    "~/Scripts/bootstrap-datepicker/js/bootstrap-datepicker.js",
                    "~/Scripts/bootstrap-daterangepicker/daterangepicker.js",
                    "~/Scripts/bootstrap-inputmask/bootstrap-inputmask.min.js"
                ));

            bundles.Add(new StyleBundle("~/Content/others").Include(
                "~/Content/zabuto_calendar.css",
                "~/Scripts/gritter/css/jquery.gritter.css",
                "~/lineicons/style.css"
                ));
        }
    }
}