using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManager.DataLayer.Repositories;
using ProductManager.Web.Services;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Factories
{
    public interface IProductCreateViewModelFactory
    {
        Task<CreateProductViewModel> CreateViewModel(int subCategoryId);
        Task<CreateProductViewModel> EditViewModel(int productId);

    }

    public class ProductCreateViewModelFactory : IProductCreateViewModelFactory
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerIdService _customerIdService;

        public ProductCreateViewModelFactory(IProductTypeRepository productTypeRepository, IProductRepository productRepository, ICustomerIdService customerIdService)
        {
            _productTypeRepository = productTypeRepository;
            _productRepository = productRepository;
            _customerIdService = customerIdService;
        }


        public async Task<CreateProductViewModel> CreateViewModel(int subCategoryId)
        {
            var productTypeViewModels = await GetProductTypeViewModels();


            return new CreateProductViewModel
            {
                SubCategoryId = subCategoryId,
                ProductTypeViewModels = productTypeViewModels
            };
        }

        private async Task<IEnumerable<ProdctTypeViewModel>> GetProductTypeViewModels()
        {
            var currentCustomerId = await _customerIdService.GetCustomerId();

            return _productTypeRepository.GetAll().Where(c=>c.CustomerId == currentCustomerId)
                .Select(x => new ProdctTypeViewModel {Description = x.Description, Id = x.Id, Name = x.Name});
        }

        public async Task<CreateProductViewModel> EditViewModel(int productId)
        {
            var currentProduct = await _productRepository.GetByIdAsync(productId);
            var productTypeViewModels = await GetProductTypeViewModels();

            var viewModel = AutoMapper.Mapper.Map<CreateProductViewModel>(currentProduct);
            viewModel.ProductTypeId = currentProduct.ProductTypeId;
            viewModel.ProductTypeViewModels = productTypeViewModels;
            
            return viewModel;
        }
    }
}