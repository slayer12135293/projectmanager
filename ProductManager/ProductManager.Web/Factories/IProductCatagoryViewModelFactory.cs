﻿using ProductManager.DataLayer.Repositories;
using ProductManager.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Web.Factories
{
   
    public interface IProductCatagoryViewModelFactory
    {
        IEnumerable<ProductCatagoryViewModel> CreateViewModel();
    }


    public class ProductCatagoryViewModelFactory : IProductCatagoryViewModelFactory
    {
        private ICategoryRepository _categoryRepository;
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
