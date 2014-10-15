using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public ProductController(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
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


        public async Task<ActionResult> Edit(int categoryId, int subCategoryId, int productId)
        {
            var model = await _productRepository.GetProductByIds(categoryId, subCategoryId, productId);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Product updatedProduct)
        {
            var categoryId = Int32.Parse(Request.QueryString["categoryId"]);
            var subCategoryId = Int32.Parse(Request.QueryString["subCategoryId"]);

            await _productRepository.UpdateProductById(categoryId, subCategoryId, updatedProduct);

            return RedirectToAction("Detail", "SubCategory", new {categoryId, subCategoryId});
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateProductViewModel createProductViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryRepository.GetByIdAsync(createProductViewModel.CategoryId);
                var currentSubCagetory = category.SubCategories.Single(x => x.Id == createProductViewModel.SubCategoryId);

                var product = new Product
                {
                    ColorName = createProductViewModel.ColorName,
                    ColoCode = createProductViewModel.ColoCode,
                    Height = createProductViewModel.Height,
                    Width = createProductViewModel.Width,
                    ProductCode = createProductViewModel.ProductCode,
                    Name = createProductViewModel.ProductName,
                    ImageUrl = createProductViewModel.ImageUrl
                };
                currentSubCagetory.Products.Add(product);
                _categoryRepository.Update(category);

                return RedirectToAction("Detail", "SubCategory", new { categoryId = createProductViewModel.CategoryId, subCategoryId= createProductViewModel.SubCategoryId });

            }

            return View("Create", createProductViewModel);
        }

        
        public async Task<ActionResult> Delete(int categoryId,int subCategoryId, int productId)
        {
            var currentCategory = await _categoryRepository.GetByIdAsync(categoryId);
            var currentSubCategory = currentCategory .SubCategories.Single(x => x.Id == subCategoryId);
            var currentProduct = currentSubCategory.Products.Single(y=>y.Id == productId);
            currentSubCategory.Products.Remove(currentProduct);
            _categoryRepository.Update(currentCategory);

            return RedirectToAction("Detail", "SubCategory", new {categoryId, subCategoryId});
        }


    }
}