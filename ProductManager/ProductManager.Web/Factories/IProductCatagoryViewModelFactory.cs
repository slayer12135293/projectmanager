using ProductManager.DataLayer.Repositories;
using ProductManager.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ProductManager.Web.Factories
{
   
    public interface IProductCategoryViewModelFactory
    {
        IEnumerable<ProductCatagoryViewModel> CreateViewModel();
    }


    public class ProductCatagoryViewModelFactory : IProductCategoryViewModelFactory
    {
        private readonly ICategoryRepository _categoryRepository;
        public ProductCatagoryViewModelFactory(ICategoryRepository categoryRepository )
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<ProductCatagoryViewModel> CreateViewModel()
        {
            var viewModel = _categoryRepository.GetAll().Select(x=> new ProductCatagoryViewModel{ CategoryId = x.Id, SubCategories= x.SubCategories, CategoryDescription= x.Description, CategoryName=x.Name } );  
            return viewModel;
        }
    }


}
