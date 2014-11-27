using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Services
{
    public interface IPricePlanService
    {
        Task<IEnumerable<PricePlanDropDownViewModel>> GetPricePlanDropDownViewModelsByIds(int currentCustomerId,int productTypeId);
    }

    public class PricePlanService : IPricePlanService
    {
        private readonly IPricePlanRepository _pricePlanRepository;
        private readonly IProductTypeRepository _productTypeRepository;

        public PricePlanService(IPricePlanRepository pricePlanRepository, IProductTypeRepository productTypeRepository)
        {
            _pricePlanRepository = pricePlanRepository;
            _productTypeRepository = productTypeRepository;
        }

        public async Task<IEnumerable<PricePlanDropDownViewModel>> GetPricePlanDropDownViewModelsByIds(int currentCustomerId, int productTypeId)
        {
            var currentProductType = _productTypeRepository.GetById(productTypeId);

            var viewModels = new List<PricePlanDropDownViewModel>();

            if (currentProductType.PriceCalculationType == PriceCalculationType.WithHeightAmount)
            {
                viewModels = await _pricePlanRepository.GetAll()
                    .Where(c => c.CustomerId == currentCustomerId && c.ProductTypeId == productTypeId)
                    .OrderBy(x => x.Name).Select(o => new PricePlanDropDownViewModel { Id = o.Id, Name = o.Name }).ToListAsync();
            }
           
            return viewModels;
        }
    }


}