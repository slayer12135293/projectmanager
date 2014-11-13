using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using ProductManager.DataLayer;
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

        public PricePlanController(ICustomerIdService customerIdService,IPricePlanRepository pricePlanRepository, IProductTypeRepository productTypeRepository, IPricePlanViewModelFactory pricePlanViewModelFactory)
        {
            _customerIdService = customerIdService;
            _pricePlanRepository = pricePlanRepository;
            _productTypeRepository = productTypeRepository;
            _pricePlanViewModelFactory = pricePlanViewModelFactory;
        }


        private CategoryDb db = new CategoryDb();

        // GET: PricePlan
        public async Task<ActionResult> Index()
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

            return View(pricePlanViewModels);
        }



        // GET: PricePlan/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var currentPricePlan = await db.PricePlans.FindAsync(id);
            if (currentPricePlan == null)
            {
                return HttpNotFound();
            }

            var viewModel = new PricePlanDetailsViewModel
            {
                PricePlan = currentPricePlan,
                PriceUnitViewModels = currentPricePlan.PriceUnits.Select(x => new PriceUnitViewModel
                {
                    PricePlanId = (int) id,
                    Id = x.Id,
                    Height = x.Height,
                    Width = x.Width,
                    Price = x.Price,
                })
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
            var currentPricePlan = await db.PricePlans.FindAsync(currentPricePlanId);

            currentPricePlan.PriceUnits.Add(priceUnit);
            await db.SaveChangesAsync();

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
            var allProductTypes = _productTypeRepository.GetAll().Where(x => x.CustomerId == currentCustomerId);
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

                db.PricePlans.Add(pricePlan);
                await db.SaveChangesAsync();
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
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PricePlan pricePlan = await db.PricePlans.FindAsync(id);
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
            PricePlan pricePlan = await db.PricePlans.FindAsync(id);
            db.PricePlans.Remove(pricePlan);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
