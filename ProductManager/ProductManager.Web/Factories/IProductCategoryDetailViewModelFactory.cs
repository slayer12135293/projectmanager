using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Web.Factories
{
    public interface IProductCategoryDetailViewModelFactory
    {
        Category CreateViewModel(int id);
    }

    public class ProductCategoryDetailViewModelFactory : IProductCategoryDetailViewModelFactory
    {
        private ICategoryRepository _categoryRepository;
        public ProductCategoryDetailViewModelFactory(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category CreateViewModel(int id)
        {
            var result = _categoryRepository.GetAll().Single(x => x.Id == id);
            var model = new Category
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                SubCategories = result.SubCategories
            };
            return model;
        }
    }
}
