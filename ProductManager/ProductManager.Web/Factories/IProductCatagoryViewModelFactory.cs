using System.Data.Entity;
using System.Threading.Tasks;
using ProductManager.DataLayer.Repositories;
using ProductManager.Web.Services;
using ProductManager.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ProductManager.Web.Factories
{
   
    public interface IProductCategoryViewModelFactory
    {
        Task<IEnumerable<ProductCatagoryViewModel>> CreateViewModel();
    }


    public class ProductCatagoryViewModelFactory : IProductCategoryViewModelFactory
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICustomerIdService _customerIdService;

        public ProductCatagoryViewModelFactory(ICategoryRepository categoryRepository, ICustomerIdService customerIdService )
        {
            _categoryRepository = categoryRepository;
            _customerIdService = customerIdService;
        }

        public async Task<IEnumerable<ProductCatagoryViewModel>> CreateViewModel()
        {
            var customerId = await _customerIdService.GetCustomerId();
            var viewModel =await _categoryRepository.GetAll().Where(i=>i.CustomerId == customerId).Select(x=> new ProductCatagoryViewModel{ CategoryId = x.Id, SubCategories= x.SubCategories, CategoryDescription= x.Description, CategoryName=x.Name } ).ToListAsync();  
            
            return viewModel;
        }
    }


}
