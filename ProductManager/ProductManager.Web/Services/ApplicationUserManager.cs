using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ProductManager.DataLayer;
using ProductManager.Enity;

namespace ProductManager.Web.Services
{

    // Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly


    public interface IUserManagerService
    {
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
        Task<ApplicationUser> FindAsync(string userName, string password);
        Task<IdentityResult> ConfirmEmailAsync(string userId, string code);
        Task<ApplicationUser> FindByNameAsync(string email);
        Task<bool> IsEmailConfirmedAsync(string id);
        Task<IdentityResult> ResetPasswordAsync(string id, string code, string password);
        Task<ApplicationUser> FindByIdAsync(string getUserId);
        Task<IdentityResult> ChangePasswordAsync(string getUserId, string oldPassword, string newPassword);
        Task<IdentityResult> AddPasswordAsync(string getUserId, string newPassword);
        Task<ApplicationUser> FindAsync(UserLoginInfo userLoginInfo);
        Task<IdentityResult> AddLoginAsync(string getUserId, UserLoginInfo login);
        Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUser applicationUser);
        Task<IList<string>> GetRolesAsync(string userId);

        Task<IdentityResult> AddToRolesAsync(string p1, string[] p2);
        Task<IdentityResult> RemoveFromRolesAsync(string userId, params string[] roles);
    }
    public class ApplicationUserManager : UserManager<ApplicationUser>, IUserManagerService
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUser applicationUser)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await CreateIdentityAsync(applicationUser, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<CategoryDb>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is: {0}"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is: {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}