using System.Collections.Generic;
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
    public class PricePlanController : Controller
    {
        private readonly ICustomerIdService _customerIdService;
        private readonly IPricePlanRepository _pricePlanRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IPricePlanViewModelFactory _pricePlanViewModelFactory;
        private readonly IProductRepository _productRepository;

        public PricePlanController(ICustomerIdService customerIdService,IPricePlanRepository pricePlanRepository, 
            IProductTypeRepository productTypeRepository, IPricePlanViewModelFactory pricePlanViewModelFactory, IProductRepository productRepository)
        {
            _customerIdService = customerIdService;
            _pricePlanRepository = pricePlanRepository;
            _productTypeRepository = productTypeRepository;
            _pricePlanViewModelFactory = pricePlanViewModelFactory;
            _productRepository = productRepository;
        }
        
     
        public  ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> GetPricePlans()
        {
            var currentCustomerId = await _customerIdService.GetCustomerId();
            var pricePlans = _pricePlanRepository.GetAll().Where(x => x.CustomerId == currentCustomerId);
            var pricePlanViewModels = new List<PricePlanViewModel>();

            // DO NOT resharper this, it doesn't work
            foreach (var plan in pricePlans)
            {
                var viewModel = _pricePlanViewModelFactory.Create(plan);
                pricePlanViewModels.Add(viewModel);
            }

            return Json(pricePlanViewModels,JsonRequestBehavior.AllowGet);
        }


        // GET: PricePlan/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var currentPricePlan = await _pricePlanRepository.GetByIdAsync(id);
            if (currentPricePlan == null)
            {
                return HttpNotFound();
            }

            var currentProductType = _productTypeRepository.GetById(currentPricePlan.ProductTypeId);

            var priceUnitViewModels = new List<PriceUnitViewModel>();

            if (currentProductType.PriceCalculationType == PriceCalculationType.WithHeightAmount)
            {
                priceUnitViewModels = currentPricePlan.PriceUnits.Select(x => new PriceUnitViewModel
                {
                    PricePlanId = id,
                    Id = x.Id,
                    Height = x.Height,
                    Width = x.Width,
                    Price = x.Price,
                }).ToList();
            }

            var viewModel = new PricePlanDetailsViewModel
            {
                PricePlan = currentPricePlan,
                PriceUnitViewModels = priceUnitViewModels
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Details(PricePlanDetailsViewModel pricePlanDetailsViewModel)
        {
            var currentUserId = await _customerIdService.GetCustomerId();

            var priceUnit = new PriceUnit
            {
                Name = pricePlanDetailsViewModel.PricePlan.Name,
                CustomerId = currentUserId,
                Height = pricePlanDetailsViewModel.PriceUnitViewModel.Height,
                Width = pricePlanDetailsViewModel.PriceUnitViewModel.Width,
                Price = pricePlanDetailsViewModel.PriceUnitViewModel.Price
            };
            var currentPricePlanId = pricePlanDetailsViewModel.PricePlan.Id;
            var currentPricePlan = await _pricePlanRepository.GetByIdAsync(currentPricePlanId);

            currentPricePlan.PriceUnits.Add(priceUnit);
            await _pricePlanRepository.Update(currentPricePlan);
            
            return RedirectToAction("Details", new {id = currentPricePlanId});

        } 

        // GET: PricePlan/Create
        public async Task<ActionResult> Create()
        {
            var allProductTypes = await GetAllProductTypes();

            return View(new CreatePricePlanViewModel {ProductTypes = allProductTypes});
        }

        private async Task<IQueryable<ProductType>> GetAllProductTypes()
        {
            var currentCustomerId = await _customerIdService.GetCustomerId();
            var allProductTypes = _productTypeRepository.GetAll().Where(x => x.CustomerId == currentCustomerId && x.PriceCalculationType == PriceCalculationType.WithHeightAmount );
            return allProductTypes;
        }

        // POST: PricePlan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreatePricePlanViewModel pricePlanViewModel)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = await _customerIdService.GetCustomerId();
                var pricePlan = new PricePlan();

                Mapper.Map(pricePlanViewModel,pricePlan);
                pricePlan.CustomerId = currentUserId;

                await _pricePlanRepository.Add(pricePlan);

                return RedirectToAction("Index");
            }

            return View(pricePlanViewModel);
        }

        // GET: PricePlan/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var pricePlan = await  _pricePlanRepository.GetByIdAsync(id);
            if (pricePlan == null)
            {
                return HttpNotFound();
            }

            var allProductTypes = await GetAllProductTypes();
            var viewModel = new EditPricePlanViewModel()
            {
                CustomerId = pricePlan.CustomerId,
                Description = pricePlan.Description,
                Name = pricePlan.Name,
                ProductTypeId = pricePlan.ProductTypeId,
                ProductTypes = allProductTypes,
                Id = pricePlan.Id

            };
            return View(viewModel);
        }

        // POST: PricePlan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditPricePlanViewModel editPricePlanViewModel)
        {
            if (ModelState.IsValid)
            {
                var currentPricePlan =await _pricePlanRepository.GetByIdAsync(editPricePlanViewModel.Id);
                currentPricePlan.ProductTypeId = editPricePlanViewModel.ProductTypeId;
                currentPricePlan.Name = editPricePlanViewModel.Name;
                currentPricePlan.Description = editPricePlanViewModel.Description;

                await _pricePlanRepository.Update(currentPricePlan);
                return RedirectToAction("Index");
            }
            return View(editPricePlanViewModel);
        }

        // GET: PricePlan/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            
            PricePlan pricePlan = await _pricePlanRepository.GetByIdAsync(id);
            if (pricePlan == null)
            {
                return HttpNotFound();
            }
            return View(pricePlan);
        }

        // POST: PricePlan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _pricePlanRepository.Remove(id);
            return RedirectToAction("Index");
        }

       
    }
}
