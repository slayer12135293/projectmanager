using System;
using System.Web.Mvc;

namespace ProductManager.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdministratorFilterAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (!filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                RedirectToPage(filterContext, "Account", "Login"); 
            }
            else
            {
                var currentUser = filterContext.HttpContext.User;
                if (currentUser.IsInRole("CustomerAdmin") || currentUser.IsInRole("SuperAdmin"))
                {
                    base.OnAuthorization(filterContext);
                }
                else
                {
                    RedirectToPage(filterContext, "UnAuthorized", "Index");
                }
            }

        }

        private static void RedirectToPage(AuthorizationContext filterContext, string controllerName, string actionName)
        {
            filterContext.RequestContext.RouteData.Values["controller"] = controllerName;
            filterContext.Result = new ViewResult { ViewName = actionName };
        }
   
    }
}
