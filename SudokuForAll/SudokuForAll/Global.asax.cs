using SudokuForAll.Engine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SudokuForAll
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            string cultura = string.Empty;
            if (System.Web.HttpContext.Current.Session["Cultura"] != null)
                cultura = System.Web.HttpContext.Current.Session["Cultura"].ToString();
            else
                System.Web.HttpContext.Current.Session["Cultura"] = EngineData.Cultura("Español");

            CultureInfo ci = new CultureInfo(EngineData.Cultura(cultura));
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = ci;
        }
    }
}
