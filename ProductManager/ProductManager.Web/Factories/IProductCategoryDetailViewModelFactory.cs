using System.Threading.Tasks;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;

namespace ProductManager.Web.Factories
{
    public interface IProductCategoryDetailViewModelFactory
    {
        Task<Category> CreateViewModel(int id);
    }

    public class ProductCategoryDetailViewModelFactory : IProductCategoryDetailViewModelFactory
    {
        private readonly ICategoryRepository _categoryRepository;
        public ProductCategoryDetailViewModelFactory(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateViewModel(int id)
        {
            var result = await _categoryRepository.GetByIdAsync(id);
            var model = new Category
            {
                Id= result.Id,
                Name = result.Name,
                Description = result.Description,
                SubCategories = result.SubCategories               
            };
            return model;
        }
    }
}
