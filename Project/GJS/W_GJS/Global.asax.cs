using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using W_GJS.Models;
namespace W_GJS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["COUNTER_VISIT_GN"] = 0;
            Application["COUNTER_VISIT_HQ"] = 0;
            Application["COUNTER_VISIT_WEEK_TN"] = 0;
            Application["COUNTER_VISIT_WEEK_TT"] = 0;
            Application["COUNTER_VISIT_MONTH_TN"] = 0;
            Application["COUNTER_VISIT_MONTH_TT"] = 0;
            Application["COUNTER_VISIT_MONTH_TOTAL"] = 0;
            Application["VISITORS_ONLINE"] = 0;
        }
        void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["VISITORS_ONLINE"] = Convert.ToInt32(Application["VISITORS_ONLINE"]) + 1;
            Application.UnLock();
            SP_STATISTICS_VISIT_Result STATISTICS_VISIT = new SP_STATISTICS_VISIT_Result();
            GJSEntities db = new GJSEntities();
            STATISTICS_VISIT = db.SP_STATISTICS_VISIT().Single<SP_STATISTICS_VISIT_Result>();
            Application["COUNTER_VISIT_GN"] = STATISTICS_VISIT.COUNTER_VISIT_GN;
            Application["COUNTER_VISIT_HQ"] = STATISTICS_VISIT.COUNTER_VISIT_HQ;
            Application["COUNTER_VISIT_WEEK_TN"] = STATISTICS_VISIT.COUNTER_VISIT_WEEK_TN;
            Application["COUNTER_VISIT_WEEK_TT"] = STATISTICS_VISIT.COUNTER_VISIT_WEEK_TT;
            Application["COUNTER_VISIT_MONTH_TN"] = STATISTICS_VISIT.COUNTER_VISIT_MONTH_TN;
            Application["COUNTER_VISIT_MONTH_TT"] = STATISTICS_VISIT.COUNTER_VISIT_MONTH_TT;
            Application["COUNTER_VISIT_MONTH_TOTAL"] = STATISTICS_VISIT.COUNTER_VISIT_TOTAL;
        }
        void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["VISITORS_ONLINE"] = Convert.ToUInt32(Application["VISITORS_ONLINE"]) - 1;
            Application.UnLock();
        }
    }
}
