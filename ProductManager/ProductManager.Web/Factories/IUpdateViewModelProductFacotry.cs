using System.Threading.Tasks;
using AutoMapper;
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

        public UpdateViewModelProductFacotry(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateProduct(CreateProductViewModel createProductViewModel)
        {
          
            var product = await _productRepository.GetByIdAsync(createProductViewModel.ProductId);

            Mapper.Map(createProductViewModel, product);

            return product;
        }
    }
}