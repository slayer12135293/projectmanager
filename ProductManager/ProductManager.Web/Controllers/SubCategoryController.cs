using System.Threading.Tasks;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.Factories;
using ProductManager.Web.Filters;
using ProductManager.Web.Services;
using ProductManager.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProductManager.Web.Controllers
{
    [AdministratorFilter]
    public class SubCategoryController : Controller
    {
        private readonly IProductSubCategoryViewModelFactory _productSubCategoryViewModelFactory;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ICustomerIdService _customerIdService;
        private readonly IPriceUnitService _priceUnitService;

        public SubCategoryController(IProductSubCategoryViewModelFactory productSubCategoryViewModelFactory, 
            ISubCategoryRepository subCategoryRepository,
            ICustomerIdService customerIdService,
            IPriceUnitService priceUnitService)
        {
            _productSubCategoryViewModelFactory = productSubCategoryViewModelFactory;
            _subCategoryRepository = subCategoryRepository;
            _customerIdService = customerIdService;
            _priceUnitService = priceUnitService;
        }

        public ActionResult Create(int categoryId)
        {
            var model = new CreateSubCategoryViewModel { CategoryId = categoryId };
            return View(model);
        }

        public async Task<ActionResult> Details( int subCategoryId)
        {
            return View(await _productSubCategoryViewModelFactory.CreateViewModel(subCategoryId));
        }

        public ActionResult DisplayPricePlan(int productId)
        {
            var viewModels = _priceUnitService.GetPricePlanByProductId(productId);
            return PartialView("PriceUnitsTableViewReadOnly", viewModels);
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

        public async Task<ActionResult> Edit(int subCategoryId)
        {
            var currentSubCategory = await _subCategoryRepository.GetByIdAsync(subCategoryId);
            var viewModel = new UpdateSubCategoryViewModel
            {
                SubCategoryId = currentSubCategory.Id,
                CategoryId = currentSubCategory.CategoryId,
                Name = currentSubCategory.Name,
                Description = currentSubCategory.Description
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UpdateSubCategoryViewModel subCategory)
        {
            if (ModelState.IsValid)
            {
                var currentSubCategory = await _subCategoryRepository.GetByIdAsync(subCategory.SubCategoryId);
                currentSubCategory.Name = subCategory.Name;
                currentSubCategory.Description = subCategory.Description;
                await _subCategoryRepository.Update(currentSubCategory);
                return RedirectToAction("Details", "Category", new {Id = subCategory.CategoryId});
            }
            return View(subCategory);
        } 


    }
}