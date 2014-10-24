using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ProductManager.DataLayer;

namespace ProductManager.Web.Services
{
    public interface IApplicationRoleManager
    {
        IQueryable<IdentityRole> Roles { get; }
    }


    public class ApplicationRoleManager : RoleManager<IdentityRole>, IApplicationRoleManager
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<CategoryDb>()));
        }
    }
}