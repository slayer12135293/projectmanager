using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.Factories;
using ProductManager.Web.ViewModels;
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
        public async Task<ActionResult> Index()
        {

            return View(await _productCatagoryViewModelFactory.CreateViewModel());
        }


        public ActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Details(int id)
        {


            ViewBag.CategoryId = id;
            return View(await _productCategoryDetailViewModelFactory.CreateViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(Category category)
        {
            _categoryRepository.Update(category);
            return RedirectToAction("Details", new {category.Id });
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


        public async Task<ActionResult> Delete(int categoryId)
        {
            var currentCategory = await _categoryRepository.GetByIdAsync(categoryId);
            await _categoryRepository.RemoveAsync(currentCategory);
            return RedirectToAction("Index");
        }


    }
}