using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.Filters;
using ProductManager.Web.Services;

namespace ProductManager.Web.Controllers
{
    [AdministratorFilter]
    public class AddOnsController : Controller
    {
        private readonly ICustomerIdService _customerIdService;
        private readonly IAddOnRepository _addOnRepository;
        private readonly IProductTypeRepository _productTypeRepository;

        public AddOnsController(ICustomerIdService customerIdService, IAddOnRepository addOnRepository, IProductTypeRepository productTypeRepository)
        {
            _customerIdService = customerIdService;
            _addOnRepository = addOnRepository;
            _productTypeRepository = productTypeRepository;
        }

        // GET: AddOns
        public async Task<ActionResult> Index()
        {
            var currentUserId =await _customerIdService.GetCustomerId();
            var addOns =await _addOnRepository.GetAll().Where(x => x.CustomerId == currentUserId).OrderBy(y=>y.ProductType.Name).ToListAsync();
            return View(addOns);
        }

        // GET: AddOns/Create
        public async Task<ActionResult> Create()
        {
            var productTypes = await GetProductTypes();

            ViewBag.ProductTypeId = new SelectList(productTypes, "Id", "Description");
            return View();
        }

        private async Task<IQueryable<ProductType>> GetProductTypes()
        {
            var currentUserId = await _customerIdService.GetCustomerId();
            var productTypes = _productTypeRepository.GetAll().Where(x => x.CustomerId == currentUserId);
            return productTypes;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ProductTypeId,Price,Name,CustomerId")] AddOn addOn)
        {
            if (ModelState.IsValid)
            {
                var customerId = await _customerIdService.GetCustomerId();
                addOn.CustomerId = customerId;
                await _addOnRepository.Add(addOn);
                return RedirectToAction("Index");
            }
            var productTypes = await GetProductTypes();
            ViewBag.ProductTypeId = new SelectList(productTypes, "Id", "Description", addOn.ProductTypeId);
            return View(addOn);
        }

        // GET: AddOns/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var addOn = await _addOnRepository.GetByIdAsync(id);
            if (addOn == null)
            {
                return HttpNotFound();
            }
            var productTypes = await GetProductTypes();
            ViewBag.ProductTypeId = new SelectList(productTypes, "Id", "Description", addOn.ProductTypeId);
            return View(addOn);
        }

        // POST: AddOns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ProductTypeId,Price,Name,CustomerId")] AddOn addOn)
        {
            if (ModelState.IsValid)
            {
                await _addOnRepository.Update(addOn);
                return RedirectToAction("Index");
            }
            var productTypes = await GetProductTypes();
            ViewBag.ProductTypeId = new SelectList(productTypes, "Id", "Description", addOn.ProductTypeId);
            return View(addOn);
        }

  
        public async Task<ActionResult> Delete(int id)
        {
            await _addOnRepository.Remove(id);
            return RedirectToAction("Index");
        }
        
    }
}
