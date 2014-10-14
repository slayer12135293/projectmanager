using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.Factories;
using ProductManager.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProductManager.Web.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductSubCategoryViewModelFactory _productSubCategoryViewModelFactory;

        public SubCategoryController(ICategoryRepository categoryRepository, IProductSubCategoryViewModelFactory productSubCategoryViewModelFactory)
        {
            _categoryRepository = categoryRepository;
            _productSubCategoryViewModelFactory = productSubCategoryViewModelFactory;
        }

        // GET: SubCategory
        public ActionResult Index()
        {
            var model = new List<ProductSubCatagoryViewMode>();
            return View(model);
        }

        public ActionResult Create(int categoryId)
        {
            var model = new CreateSubCategoryViewModel { CategoryId = categoryId };
            return View(model);
        }

        public ActionResult Detail(int categoryId, int subCategoryId)
        {

            ViewData["catagoryId"] = categoryId;
            return View(_productSubCategoryViewModelFactory.CreateViewModel(categoryId, subCategoryId));
        }


        [HttpPost]
        public ActionResult Create(CreateSubCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var currentCategory = _categoryRepository.GetById(viewModel.CategoryId);
                var subCategory = new SubCategory { Id = viewModel.CategoryId, Name = viewModel.Name, Description = viewModel.Description };
                currentCategory.SubCategories.Add(subCategory);
                _categoryRepository.Update(currentCategory);
                return RedirectToAction("index", "Category");

            }

            return View(viewModel);
        }

    }
}