using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.Factories;
using ProductManager.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProductManager.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductCategoryViewModelFactory _productCatagoryViewModelFactory;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductCategoryDetailViewModelFactory _productCategoryDetailViewModelFactory;
        
        public CategoryController(IProductCategoryViewModelFactory productCatagoryViewModelFactory, 
            ICategoryRepository categoryRepository,
            IProductCategoryDetailViewModelFactory productCategoryDetailViewModelFactory)
        {
            _productCatagoryViewModelFactory = productCatagoryViewModelFactory;
            _categoryRepository = categoryRepository;
            _productCategoryDetailViewModelFactory = productCategoryDetailViewModelFactory;
        }
        // GET: Category
        public ActionResult Index()
        {
            return View(_productCatagoryViewModelFactory.CreateViewModel());
        }


        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View(_productCategoryDetailViewModelFactory.CreateViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Category category)
        {
            _categoryRepository.Update(category);
            return RedirectToAction("Details", new { Id = category.Id });
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCategoryViewModel category)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index"); 
            var item = new Category { Name = category.Name, Description = category.Description };
            _categoryRepository.Add(item);
            return RedirectToAction("Index");
        }

    }
}