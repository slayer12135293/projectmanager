using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.Filters;
using ProductManager.Web.Services;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Controllers
{
    [AdministratorFilter]
    public class CustomerProfileController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerIdService _customerIdService;

        public CustomerProfileController(ICustomerRepository customerRepository, ICustomerIdService customerIdService)
        {
            _customerRepository = customerRepository;
            _customerIdService = customerIdService;
        }

        // GET: CustomerProfile
        public async Task<ActionResult> Edit()
        {
            var currentCustomer = await GetCurrentCustomer();
            var viewModel = new UpdateCustomerInfoViewModel();
            Mapper.Map(currentCustomer,viewModel);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UpdateCustomerInfoViewModel updateCustomerInfo)
        {
            if (ModelState.IsValid)
            {
                var currentCustomer = await GetCurrentCustomer();
                Mapper.Map(updateCustomerInfo, currentCustomer);
                await _customerRepository.Update(currentCustomer);

                return RedirectToAction("Index", "Home");
            }
            return View(updateCustomerInfo);

        }

        private async Task<Customer> GetCurrentCustomer()
        {
            var currentCustomerId = await _customerIdService.GetCustomerId();
            var currentCustomer = await _customerRepository.GetByIdAsync(currentCustomerId);
            return currentCustomer;
        }
    }
}