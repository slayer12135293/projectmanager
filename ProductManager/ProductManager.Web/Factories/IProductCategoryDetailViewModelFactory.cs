using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;

namespace ProductManager.Web.Factories
{
    public interface IProductCategoryDetailViewModelFactory
    {
        Task<CategoryDetailsViewModel> CreateViewModel(int id);
    }

    public class ProductCategoryDetailViewModelFactory : IProductCategoryDetailViewModelFactory
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;

        public ProductCategoryDetailViewModelFactory(ICategoryRepository categoryRepository, ISubCategoryRepository subCategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _subCategoryRepository = subCategoryRepository;
        }

    

        public async Task<CategoryDetailsViewModel> CreateViewModel(int id)
        {
            var subCategories = await _subCategoryRepository.GetSubCategoriesByCategoryId(id);
            var category =await _categoryRepository.GetByIdAsync(id);
            return new CategoryDetailsViewModel{ Category = category, SubCategories = subCategories};
        }
    }

    public class CategoryDetailsViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<SubCategory> SubCategories { get; set; }
    }
}
