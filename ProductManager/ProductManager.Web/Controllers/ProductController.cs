using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.Services;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerIdService _customerIdService;

        public ProductController(IProductRepository productRepository, ICustomerIdService customerIdService)
        {
            _productRepository = productRepository;
            _customerIdService = customerIdService;
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create(int categoryId, int subCategoryId)
        {
            var model = new CreateProductViewModel {SubCategoryId = subCategoryId, CategoryId = categoryId};
            return View(model);
        }


        public async Task<ActionResult> Edit(int productId)
        {
            var model = await _productRepository.GetByIdAsync(productId);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Product updatedProduct)
        {
            await _productRepository.Update(updatedProduct);
            var subCategoryId = updatedProduct.SubCategoryId;
            var categoryId = updatedProduct.SubCategory.CategoryId;
            return RedirectToAction("Detail", "SubCategory", new {categoryId, subCategoryId});
        }


     
        public async Task<ActionResult> Delete(int categoryId, int subCategoryId, int productId)
        {
            await _productRepository.Remove(productId);
            return RedirectToAction("Detail", "SubCategory", new { categoryId, subCategoryId });
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateProductViewModel createProductViewModel)
        {
            if (ModelState.IsValid)
            {   
                var product = new Product
                {
                    SubCategoryId =  createProductViewModel.SubCategoryId,
                    ColorName = createProductViewModel.ColorName,
                    ColoCode = createProductViewModel.ColoCode,
                    Height = createProductViewModel.Height,
                    Width = createProductViewModel.Width,
                    ProductCode = createProductViewModel.ProductCode,
                    Name = createProductViewModel.ProductName,
                    ImageUrl = createProductViewModel.ImageUrl,
                    CustomerId = await _customerIdService.GetCustomerId()
                };
       

                await _productRepository.Add(product);


                return RedirectToAction("Detail", "SubCategory", new { categoryId = createProductViewModel.CategoryId, subCategoryId= createProductViewModel.SubCategoryId });

            }

            return View("Create", createProductViewModel);
        }

    }
}