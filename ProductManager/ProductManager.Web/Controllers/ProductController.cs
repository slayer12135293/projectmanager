using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
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
        private readonly IProductTypeRepository _productTypeRepository;

        public ProductController(IProductRepository productRepository, ICustomerIdService customerIdService, IProductCreateViewModelFactory productCreateViewModelFactory, IUpdateViewModelProductFacotry updateViewModelProductFacotry, IProductTypeRepository productTypeRepository)
        {
            _productRepository = productRepository;
            _customerIdService = customerIdService;
            _productCreateViewModelFactory = productCreateViewModelFactory;
            _updateViewModelProductFacotry = updateViewModelProductFacotry;
            _productTypeRepository = productTypeRepository;
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

      
            //var product = await _productRepository.GetByIdAsync(createProductViewModel.ProductId);
            //product.ImageUrl = createProductViewModel.ImageUrl;
            //product.IsNewProduct = createProductViewModel.IsNewProduct;
            //product.Name = createProductViewModel.Name;
            //product.UnitPrice = createProductViewModel.UnitPrice;
            //product.Width = createProductViewModel.Width;
            //product.Height = createProductViewModel.Height;
            //product.ColoCode = createProductViewModel.ColoCode;
            //product.ColorName = createProductViewModel.ColorName;
            //product.ProductTypeId = createProductViewModel.ProductType;

            
            
            
            //await _productRepository.Update(product);
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
                var product = new Product
                {
                    SubCategoryId = createProductViewModel.SubCategoryId,
                    ColorName = createProductViewModel.ColorName,
                    ColoCode = createProductViewModel.ColoCode,
                    Height = createProductViewModel.Height,
                    Width = createProductViewModel.Width,
                    ProductCode = createProductViewModel.ProductCode,
                    Name = createProductViewModel.Name,
                    ImageUrl = createProductViewModel.ImageUrl,
                    CustomerId = await _customerIdService.GetCustomerId(),
                    ProductTypeId = createProductViewModel.ProductType
                };


                await _productRepository.Add(product);


                return RedirectToAction("Detail", "SubCategory", new { subCategoryId = createProductViewModel.SubCategoryId });

            }

            return View("Create", createProductViewModel);
        }

        public async Task<ActionResult> AllProducts(int subcategoryId)
        {
            var products = await _productRepository.GetProductsFromSubCategory(subcategoryId);
            var productsViewModels = AutoMapper.Mapper.Map<IEnumerable<ProductViewModel>>(products);
            return Json(productsViewModels.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}