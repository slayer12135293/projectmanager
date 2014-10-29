using System.Threading.Tasks;
using System.Web.Routing;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.Factories;
using ProductManager.Web.Services;
using ProductManager.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProductManager.Web.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly IProductSubCategoryViewModelFactory _productSubCategoryViewModelFactory;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ICustomerIdService _customerIdService;

        public SubCategoryController(IProductSubCategoryViewModelFactory productSubCategoryViewModelFactory, 
            ISubCategoryRepository subCategoryRepository,
            ICustomerIdService customerIdService)
        {
            _productSubCategoryViewModelFactory = productSubCategoryViewModelFactory;
            _subCategoryRepository = subCategoryRepository;
            _customerIdService = customerIdService;
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

        public async Task<ActionResult> Detail(int categoryId, int subCategoryId)
        {
            ViewData["catagoryId"] = categoryId;
            return View(await _productSubCategoryViewModelFactory.CreateViewModel(subCategoryId));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateSubCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var subCategory = new SubCategory { CategoryId = viewModel.CategoryId, Name = viewModel.Name, Description = viewModel.Description, CustomerId = await _customerIdService.GetCustomerId()};
                await _subCategoryRepository.Add(subCategory);
                return RedirectToAction("Details", "Category", new {Id = viewModel.CategoryId});
            }

            return View(viewModel);
        }


        public async Task<ActionResult> Delete(int subCategoryId)
        {
            await _subCategoryRepository.Remove(subCategoryId);
            return RedirectToAction("Index", "Category");
        } 


    }
}