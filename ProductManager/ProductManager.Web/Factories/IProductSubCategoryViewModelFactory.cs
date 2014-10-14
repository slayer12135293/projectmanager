using System.Linq;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;

namespace ProductManager.Web.Factories
{
    public interface IProductSubCategoryViewModelFactory
    {
        SubCategory CreateViewModel(int categoryId, int subCategoryId);
    }

    public class ProductSubCategoryViewModelFactory : IProductSubCategoryViewModelFactory
    {
        private readonly ICategoryRepository _categoryRepository;

        public ProductSubCategoryViewModelFactory(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public SubCategory CreateViewModel(int categoryId, int subCategoryId)
        {
            var currentCategory = _categoryRepository.GetById(categoryId);
            var currentSubCategory = currentCategory.SubCategories.Single(x => x.Id == subCategoryId);
            
            //TODO use a viewmodel instead
            return new SubCategory
            {
                Id = currentSubCategory.Id,
                Description = currentSubCategory.Description,
                Name = currentSubCategory.Name,
                Products = currentSubCategory.Products.ToList()
            };
        }
    }
}