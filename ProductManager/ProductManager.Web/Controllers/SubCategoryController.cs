using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManager.Web.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public SubCategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: SubCategory
        public ActionResult Index()
        {
            var model = new List<ProductSubCatagoryViewMode>();
            return View(model);
        }

        public ActionResult Create(int categoryId)
        {
            var model = new CreateSubCategoryViewModel() { CategoryId = categoryId };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateSubCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var currentCategory = _categoryRepository.GetById(viewModel.CategoryId);
                var subCategory = new SubCategory {Id=viewModel.CategoryId, Name = viewModel.Name, Descrition = viewModel.Description };
                currentCategory.SubCategories.Add(subCategory);
                _categoryRepository.Update(currentCategory);
                return RedirectToAction("index", "Category");

            }

            return View(viewModel);
        }

    }
}