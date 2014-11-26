using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ProductManager.DataLayer.Repositories;
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

        public PricePlanService(IPricePlanRepository pricePlanRepository)
        {
            _pricePlanRepository = pricePlanRepository;
        }

        public async Task<IEnumerable<PricePlanDropDownViewModel>> GetPricePlanDropDownViewModelsByIds(int currentCustomerId, int productTypeId)
        {
            var result = await _pricePlanRepository.GetAll().Where(c => c.CustomerId == currentCustomerId)
            .OrderBy(x => x.Name)
            .Where(y => y.ProductTypeId == productTypeId)
            .Select(o => new PricePlanDropDownViewModel { Id = o.Id, Name = o.Name }).ToListAsync();
            return result;
        }
    }


}