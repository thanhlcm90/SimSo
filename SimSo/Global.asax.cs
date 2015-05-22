using SimSo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SimSo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private StatisticRepo statistic = null;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(Object sender, EventArgs e)
        {
            statistic = new StatisticRepo();
            statistic.InsertOrUpdate();
            statistic.SetCurrentCount(1);
            statistic.Dispose();
        }

        protected void Session_End(Object sender, EventArgs e)
        {
            statistic = new StatisticRepo();
            statistic.SetCurrentCount(-1);
            statistic.Dispose();
        }
    }
}
