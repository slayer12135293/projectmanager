using System.Threading.Tasks;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Factories
{
    public interface IUpdateViewModelProductFacotry
    {
        Task<Product> CreateProduct(CreateProductViewModel createProductViewModel);
    }

    public class UpdateViewModelProductFacotry : IUpdateViewModelProductFacotry
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IProductRepository _productRepository;

        public UpdateViewModelProductFacotry(IProductTypeRepository productTypeRepository, IProductRepository productRepository)
        {
            _productTypeRepository = productTypeRepository;
            _productRepository = productRepository;
        }

        public async Task<Product> CreateProduct(CreateProductViewModel createProductViewModel)
        {
          
            var updatedProductType = await _productTypeRepository.GetByIdAsync(createProductViewModel.ProductType);

            var po = await _productRepository.GetByIdAsync(createProductViewModel.ProductId);
            var currentSubCat = po.SubCategory;
            //product.ImageUrl = createProductViewModel.ImageUrl;
            //product.IsNewProduct = createProductViewModel.IsNewProduct;
            //product.Name = createProductViewModel.Name;
            //product.UnitPrice = createProductViewModel.UnitPrice;
            //product.Width = createProductViewModel.Width;
            //product.Height = createProductViewModel.Height;
            //product.ColoCode = createProductViewModel.ColoCode;
            //product.ColorName = createProductViewModel.ColorName;

            var product = AutoMapper.Mapper.Map<Product>(createProductViewModel);
            product.ProductTypeId = createProductViewModel.ProductType;
            product.ProductType = updatedProductType;
            product.SubCategoryId = createProductViewModel.SubCategoryId;
            product.SubCategory = currentSubCat;

            return product;
        }
    }
}