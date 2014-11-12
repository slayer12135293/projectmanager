using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.Factories;
using ProductManager.Web.Services;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Controllers
{
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
            var pricePlansViewModels = await
                _pricePlanRepository.GetAll()
                    .OrderBy(x => x.Name)
                    .Where(y => y.Id == productTypeId)
                    .Select(o => new PricePlanDropDownViewModel {Id = o.Id, Name = o.Name}).ToListAsync();
           // var pricePlanViewModes = pricePlans.Select(x => new PricePlanDropDownViewModel {Id = x.Id, Name = x.Name});
            return Json(pricePlansViewModels, JsonRequestBehavior.AllowGet);
        }



        // GET: Product
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create(int subCategoryId)
        {
            var model = _productCreateViewModelFactory.CreateViewModel(subCategoryId);
            return View(model);
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

            return View("Create", createProductViewModel);
        }

        public async Task<ActionResult> AllProducts(int subcategoryId)
        {
            var products = await _productRepository.GetProductsFromSubCategory(subcategoryId);
            var productsViewModels = Mapper.Map<IEnumerable<ProductViewModel>>(products);
            return Json(productsViewModels.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}