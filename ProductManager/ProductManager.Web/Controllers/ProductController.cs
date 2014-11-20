using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.Factories;
using ProductManager.Web.Filters;
using ProductManager.Web.Services;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Controllers
{
    [AdministratorFilter]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerIdService _customerIdService;
        private readonly IProductCreateViewModelFactory _productCreateViewModelFactory;
        private readonly IUpdateViewModelProductFacotry _updateViewModelProductFacotry;
        private readonly IPricePlanRepository _pricePlanRepository;

        public ProductController(IProductRepository productRepository, ICustomerIdService customerIdService, IProductCreateViewModelFactory productCreateViewModelFactory, IUpdateViewModelProductFacotry updateViewModelProductFacotry, IPricePlanRepository pricePlanRepository )
        {
            _productRepository = productRepository;
            _customerIdService = customerIdService;
            _productCreateViewModelFactory = productCreateViewModelFactory;
            _updateViewModelProductFacotry = updateViewModelProductFacotry;
            _pricePlanRepository = pricePlanRepository;
        }


        public async Task<ActionResult> PricePlans(int productTypeId)
        {
            var currentCustomerId = await _customerIdService.GetCustomerId();


            var pricePlansViewModels = _pricePlanRepository.GetAll().Where(c=>c.CustomerId == currentCustomerId)
                    .OrderBy(x => x.Name)
                    .Where(y => y.ProductTypeId == productTypeId)
                    .Select(o => new PricePlanDropDownViewModel {Id = o.Id, Name = o.Name}).ToList();
            return Json(pricePlansViewModels, JsonRequestBehavior.AllowGet);
        }



        // GET: Product
        public ActionResult Index()
        {
            return View();
        }




        public async Task<ActionResult> Edit(int productId)
        {
            var viewModel = await _productCreateViewModelFactory.EditViewModel(productId);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CreateProductViewModel createProductViewModel)
        {
            var product = await _updateViewModelProductFacotry.CreateProduct(createProductViewModel);
            await _productRepository.Update(product);
            return RedirectToAction("Detail", "SubCategory", new { createProductViewModel.SubCategoryId });
        }

        public async Task<ActionResult> Delete(int subCategoryId, int productId)
        {
            await _productRepository.Remove(productId);
            return RedirectToAction("Detail", "SubCategory", new { subCategoryId });
        }


        public async Task<ActionResult> Create(int subCategoryId)
        {
            var model = await _productCreateViewModelFactory.CreateViewModel(subCategoryId);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateProductViewModel createProductViewModel)
        {
            if (ModelState.IsValid)
            {
                var product = new Product();
                Mapper.Map(createProductViewModel, product);
                product.CustomerId = await _customerIdService.GetCustomerId();
                await _productRepository.Add(product);
                return RedirectToAction("Detail", "SubCategory", new { subCategoryId = createProductViewModel.SubCategoryId });

            }

            return RedirectToAction("Create", new { subCategoryId = createProductViewModel.SubCategoryId});
        }

        public async Task<ActionResult> AllProducts(int subcategoryId)
        {
            var products = await _productRepository.GetProductsFromSubCategory(subcategoryId);
            var productsViewModels = Mapper.Map<IEnumerable<ProductViewModel>>(products);
            return Json(productsViewModels.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}