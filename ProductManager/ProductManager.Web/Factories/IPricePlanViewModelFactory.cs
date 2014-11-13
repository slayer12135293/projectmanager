using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Factories
{
    public interface IPricePlanViewModelFactory
    {
        PricePlanViewModel Create(PricePlan pricePlan);
    }

    public class PricePlanViewModelFactory : IPricePlanViewModelFactory
    {
        private readonly IProductTypeRepository _productTypeRepository;


        public PricePlanViewModelFactory(IProductTypeRepository productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }


        public PricePlanViewModel Create(PricePlan pricePlan)
        {
            return  new PricePlanViewModel()
            {
                Description = pricePlan.Description,
                Id = pricePlan.Id,
                Name = pricePlan.Name,
                ProductTypeId = pricePlan.ProductTypeId,
                ProductTypeName = GetProductTypeNameById(pricePlan.ProductTypeId)
            };
           
        }

        private string GetProductTypeNameById(int productTypeId)
        {
            var productType = _productTypeRepository.GetById(productTypeId);
            return productType.Name;
        }


    }

}