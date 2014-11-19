using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.Filters;
using ProductManager.Web.Services;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Controllers
{
    [AdministratorFilter]
    public class ProductTypesController : Controller
    {
        private readonly ICustomerIdService _customerIdService;
        private readonly IProductTypeRepository _productTypeRepository;

        public ProductTypesController(ICustomerIdService customerIdService, IProductTypeRepository productTypeRepository)
        {
            _customerIdService = customerIdService;
            _productTypeRepository = productTypeRepository;
        }

        // GET: ProductTypes
        public async Task<ActionResult> Index()
        {
            var currentCustomerId = await _customerIdService.GetCustomerId();
            return View(await  _productTypeRepository.GetAll().Where(x => x.CustomerId == currentCustomerId).ToListAsync());
        }

        public async Task<ActionResult> GetAllTypes()
        {
            var currentCustomerId = await _customerIdService.GetCustomerId();
            return
                Json(
                    await
                        _productTypeRepository.GetAll()
                            .Where(x => x.CustomerId == currentCustomerId)
                            .OrderBy(y => y.Name).Select(z=> new ProductTypeViewModel
                            {
                                CustomerId = z.CustomerId,
                                Description = z.Description,
                                Name = z.Name,
                                Id = z.Id
                            })
                            .ToListAsync(), JsonRequestBehavior.AllowGet);
        } 


        // GET: ProductTypes/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var productType = await  _productTypeRepository.GetByIdAsync(id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            return View(productType);
        }

        // GET: ProductTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,Name,CustomerId")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                var customerId = await _customerIdService.GetCustomerId();
                productType.CustomerId = customerId;

                await _productTypeRepository.Add(productType);
                return RedirectToAction("Index");
            }

            return View(productType);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var productType = await _productTypeRepository.GetByIdAsync(id);
            if (productType == null)
            {
                return HttpNotFound();
            }
            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,Name,CustomerId")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                await _productTypeRepository.Update(productType);
                return RedirectToAction("Index");
            }
            return View(productType);
        }

        public async Task<ActionResult> Delete(int id)
        {
            await _productTypeRepository.Remove(id);
            return RedirectToAction("Index");
        }

    }
}
