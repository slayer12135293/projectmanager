using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace ProductManager.Web.Services
{
    public interface ICustomerIdService
    {
        Task<int> GetCustomerId();
    }

    public class CustomerIdService :ICustomerIdService {
        private readonly IUserManagerService _userManagerService;
        public CustomerIdService(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }

        public async Task<int> GetCustomerId()
        {
            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            var currentUser = await _userManagerService.FindByIdAsync(currentUserId);
            return currentUser.CustomerId;
        }
    }
}