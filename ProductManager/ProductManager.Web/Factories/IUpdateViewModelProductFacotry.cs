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
        private readonly IProductRepository _productRepository;

        public UpdateViewModelProductFacotry(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateProduct(CreateProductViewModel createProductViewModel)
        {
          
            var product = await _productRepository.GetByIdAsync(createProductViewModel.ProductId);
            product.ImageUrl = createProductViewModel.ImageUrl;
            product.IsNewProduct = createProductViewModel.IsNewProduct;
            product.Name = createProductViewModel.Name;
            product.UnitPrice = createProductViewModel.UnitPrice;
            product.Width = createProductViewModel.Width;
            product.Height = createProductViewModel.Height;
            product.ColoCode = createProductViewModel.ColoCode;
            product.ColorName = createProductViewModel.ColorName;
            product.ProductTypeId = createProductViewModel.ProductType;

            await _productRepository.Update(product);
            return product;
        }
    }
}