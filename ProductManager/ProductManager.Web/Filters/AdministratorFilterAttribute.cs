using System;
using System.Web.Mvc;

namespace ProductManager.Web.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdministratorFilterAttribute : AuthorizeAttribute
    {
        public string RedirectUrl = "~/UnAuthorized/Index";
        
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var currentUser = filterContext.HttpContext.User;
            if (!currentUser.IsInRole("CustomerAdmin") || !currentUser.IsInRole("SuperAdmin"))
            {
                filterContext.RequestContext.HttpContext.Response.Redirect(RedirectUrl);
                
            }
            
        }
    }
}
