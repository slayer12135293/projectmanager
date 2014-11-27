using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.Services;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Factories
{
    public interface IProductCreateViewModelFactory
    {
        Task<CreateProductViewModel> CreateViewModel(int subCategoryId);
        Task<EditProductViewModel> EditViewModel(int productId);

    }

    public class ProductCreateViewModelFactory : IProductCreateViewModelFactory
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerIdService _customerIdService;
        private readonly IPricePlanService _pricePlanService;

        public ProductCreateViewModelFactory(IProductTypeRepository productTypeRepository, IProductRepository productRepository, ICustomerIdService customerIdService, IPricePlanService pricePlanService)
        {
            _productTypeRepository = productTypeRepository;
            _productRepository = productRepository;
            _customerIdService = customerIdService;
            _pricePlanService = pricePlanService;
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

        public async Task<EditProductViewModel> EditViewModel(int productId)
        {
            var currentCustomerId = await _customerIdService.GetCustomerId();
            var currentProduct = await _productRepository.GetByIdAsync(productId);
            var productTypeViewModels = await GetProductTypeViewModels();
            var currentProductType = _productTypeRepository.GetById(currentProduct.ProductTypeId);

            var pricePlanViewModels = new List<PricePlanDropDownViewModel>();
            var usePricePlan = false;

            if (currentProductType.PriceCalculationType == PriceCalculationType.WithHeightAmount)
            {
                var result = await _pricePlanService.GetPricePlanDropDownViewModelsByIds(currentCustomerId, currentProduct.ProductTypeId);
                pricePlanViewModels = result.ToList();
                usePricePlan = true;
            }


            var viewModel = AutoMapper.Mapper.Map<EditProductViewModel>(currentProduct);
            viewModel.ProductTypeId = currentProduct.ProductTypeId;
            viewModel.ProductTypeViewModels = productTypeViewModels;
            viewModel.PricePlanViewModels = pricePlanViewModels;
            viewModel.UsingPricePlan = usePricePlan;
            
            return viewModel;
        }
    }
}