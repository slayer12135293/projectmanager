using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ProductManager.DataLayer.Repositories;

namespace ProductManager.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MappingInitalizer.CreateMaps();

            var engine = new ExtendedRazorViewEngine();
            engine.AddPartialViewLocationFormat("~/Views/Shared/Partials/{0}.cshtml");
            ViewEngines.Engines.Add(engine);
        }
    }

    public class ExtendedRazorViewEngine : RazorViewEngine
    {
        #region Methods

        public void AddViewLocationFormat(string paths)
        {
            var existingPaths = new List<string>(ViewLocationFormats) { paths };

            ViewLocationFormats = existingPaths.ToArray();
        }

        public void AddPartialViewLocationFormat(string paths)
        {
            var existingPaths = new List<string>(PartialViewLocationFormats) { paths };

            PartialViewLocationFormats = existingPaths.ToArray();
        }

        #endregion
    }
}
