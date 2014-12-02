using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductManager.DataLayer.Repositories;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Services
{
    public interface IPriceUnitService
    {
        Task<IEnumerable<PriceUnitViewModel>> GetPricePlanByProductId(int productId);
    }

    public class PriceUnitService : IPriceUnitService
    {
        private readonly IProductRepository _productRepository;
        private readonly IPricePlanRepository _pricePlanRepository;

        public PriceUnitService(IProductRepository productRepository, IPricePlanRepository pricePlanRepository)
        {
            _productRepository = productRepository;
            _pricePlanRepository = pricePlanRepository;
        }

        public async Task<IEnumerable<PriceUnitViewModel>> GetPricePlanByProductId(int productId)
        {
            var currentProduct = await _productRepository.GetByIdAsync(productId);
            var currentPricePlan = await _pricePlanRepository.GetByIdAsync(currentProduct.PricePlanId);

            return currentPricePlan.PriceUnits.Select(x => new PriceUnitViewModel
            {
                PricePlanId = currentProduct.CustomerId,
                Id = x.Id,
                Height = x.Height,
                Width = x.Width,
                Price = x.Price,
            }).ToList();
        }
    }
}