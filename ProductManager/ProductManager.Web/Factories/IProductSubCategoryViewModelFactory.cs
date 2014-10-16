using System.Linq;
using System.Threading.Tasks;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;

namespace ProductManager.Web.Factories
{
    public interface IProductSubCategoryViewModelFactory
    {
        Task<SubCategory> CreateViewModel(int categoryId, int subCategoryId);
    }

    public class ProductSubCategoryViewModelFactory : IProductSubCategoryViewModelFactory
    {
        private readonly ISubCategoryRepository _subCategoryRepository;

        public ProductSubCategoryViewModelFactory(ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task<SubCategory> CreateViewModel(int categoryId, int subCategoryId)
        {
           
            var currentSubCategory = await _subCategoryRepository.GetSubCategoryByIds(categoryId, subCategoryId);
          
            //TODO use a viewmodel instead
            return new SubCategory
            {
                Id = currentSubCategory.Id,
                Description =  currentSubCategory.Description,
                Name = currentSubCategory.Name,
                Products = currentSubCategory.Products.ToList()
            };
        }
    }
}