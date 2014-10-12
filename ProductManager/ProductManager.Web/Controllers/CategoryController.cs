using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.Factories;
using ProductManager.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManager.Web.Controllers
{
    public class CategoryController : Controller
    {
        private IProductCatagoryViewModelFactory _productCatagoryViewModelFactory;
        private ICategoryRepository _categoryRepository;
        public CategoryController(IProductCatagoryViewModelFactory productCatagoryViewModelFactory, ICategoryRepository categoryRepository)
        {
            _productCatagoryViewModelFactory = productCatagoryViewModelFactory;
            _categoryRepository = categoryRepository;
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                var item = new Category { Name = category.Name, Description = category.Description };
                _categoryRepository.Add(item);
                     
            }
            return RedirectToAction("Index");
        }

    }
}