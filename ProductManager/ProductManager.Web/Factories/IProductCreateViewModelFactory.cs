using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManager.DataLayer.Repositories;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Factories
{
    public interface IProductCreateViewModelFactory
    {
        CreateProductViewModel CreateViewModel(int subCategoryId);
        Task<CreateProductViewModel> EditViewModel(int productId);

    }

    public class ProductCreateViewModelFactory : IProductCreateViewModelFactory
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IProductRepository _productRepository;

        public ProductCreateViewModelFactory(IProductTypeRepository productTypeRepository, IProductRepository productRepository)
        {
            _productTypeRepository = productTypeRepository;
            _productRepository = productRepository;
        }


        public CreateProductViewModel CreateViewModel(int subCategoryId)
        {
            var productTypeViewModels = GetProductTypeViewModels();

            return new CreateProductViewModel
            {
                SubCategoryId = subCategoryId,
                ProductTypeViewModels = productTypeViewModels
            };
        }

        private IEnumerable<ProdctTypeViewModel> GetProductTypeViewModels()
        {
            return _productTypeRepository.GetAll()
                .Select(x => new ProdctTypeViewModel {Description = x.Description, Id = x.Id, Name = x.Name});
        }

        public async Task<CreateProductViewModel> EditViewModel(int productId)
        {
            var currentProduct = await _productRepository.GetByIdAsync(productId);
            var productTypeViewModels = GetProductTypeViewModels();

            var viewModel = AutoMapper.Mapper.Map<CreateProductViewModel>(currentProduct);
            viewModel.ProductType = currentProduct.ProductType.Id;
            viewModel.ProductTypeViewModels = productTypeViewModels;
            
            return viewModel;
        }
    }
}