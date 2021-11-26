using System.Web.Optimization;
using System.Web.UI;

namespace GoTSkillZ.Web.UI.Site
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/frameworks").Include(
                "~/Scripts/jquery-3.4.1.min.js",
                "~/Scripts/underscore-min.js",
                "~/Scripts/boostrap.min.js",
                "~/Scripts/jquery-ui/ui/widgets/datepicker-min.js",
                "~/Scripts/ResizeSensor-min.js",
                "~/Scripts/perfect-scrollbar/perfect-scrollbar.min.js",
                "~/Scripts/moment/min/moment.min.js",
                "~/Scripts/popper.js/popper.min.js",
                "~/Scripts/bracket.js",
                "~/Scripts/Bootstrap-4-Hullabaloo/hullabaloo.min.js",
                "~/Scripts/peity/peity.min.js",
                "~/Scripts/bootstrap4-Toggle/bootstrap4-toggle.min.js"



            ));


            bundles.Add(new ScriptBundle("~/bundles/Auth").Include(
                "~/CustomScripts/Utility/Utilities.js",
                "~/Auth/Auth.js"
            ));


            bundles.Add(new ScriptBundle("~/bundles/core").Include(
                "~/CustomScripts/DTO/DTO.js",
                "~/CustomScripts/STO/STO.js",
                "~/CustomScripts/Utility/CommonInitializers.js",
                "~/CustomScripts/Utility/NavigationBuilder.js",
                "~/CustomScripts/Utility/GateKeeper.js",
                "~/CustomScripts/Google/YouTube.js",
                "~/Scripts/CookieAlert/CookieAlert.js",
                "~/CustomScripts/Home/Home.js"
            ));


            bundles.Add(new ScriptBundle("~/bundles/codemirror").Include(
                "~/Scripts/codemirror/lib/codemirror.js",
                "~/Scripts/codemirror/mode/javascript/javascript.js",
                "~/Scripts/codemirror/addon/scroll/simplescrollbars.js"
            ));


             
                bundles.Add(new ScriptBundle("~/bundles/Kendo").Include(
                    "~/Scripts/Kendo/jszip.min.js",
                    "~/Scripts/Kendo/kendo.all.min.js"
                ));

                bundles.Add(new ScriptBundle("~/bundles/summernote").Include(
                    "~/Scripts/SummerNote/summernote-bs4.min.js"
                ));

                bundles.Add(new ScriptBundle("~/bundles/profile").Include(
                    "~/Scripts/parsleyjs/parsley.min.js",
                    "~/Scripts/jquery-steps/jquery.steps.min.js"
                ));


            ScriptManager.ScriptResourceMapping.AddDefinition(
                "respond",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/respond.min.js",
                    DebugPath = "~/Scripts/respond.js"
                });

            BundleTable.EnableOptimizations = false;
        }
    }
}