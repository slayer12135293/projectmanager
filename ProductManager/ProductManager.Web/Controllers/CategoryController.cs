using ProductManager.Web.Factories;
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
        public CategoryController(IProductCatagoryViewModelFactory productCatagoryViewModelFactory)
        {
            _productCatagoryViewModelFactory = productCatagoryViewModelFactory;

        }
        // GET: Category
        public ActionResult Index()
        {
            return View(_productCatagoryViewModelFactory.CreateViewModel());
        }
    }
}