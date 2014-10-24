using System;
using System.Web.Mvc;

namespace ProductManager.Web.Filters
{
    public class AdministratorFilterAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var currentUser = filterContext.HttpContext.User;
            if (!currentUser.IsInRole("CustomerAdmin") || !currentUser.IsInRole("SuperAdmin"))
            {
                throw new UnauthorizedAccessException();
            }
            
        }
    }
}
