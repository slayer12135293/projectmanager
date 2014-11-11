using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using ProductManager.DataLayer;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.Services;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Controllers
{
    public class PricePlanController : Controller
    {
        private readonly ICustomerIdService _customerIdService;
        private readonly IProductTypeRepository _productTypeRepository;

        public PricePlanController(ICustomerIdService customerIdService, IProductTypeRepository productTypeRepository)
        {
            _customerIdService = customerIdService;
            _productTypeRepository = productTypeRepository;
        }


        private CategoryDb db = new CategoryDb();

        // GET: PricePlan
        public async Task<ActionResult> Index()
        {
            return View(await db.PricePlans.ToListAsync());
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
                    Height = x.Height,
                    Width = x.Width,
                    Price = x.Price
                })
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Details(PricePlanDetailsViewModel pricePlanDetailsViewModel)
        {
            var priceUnit = new PriceUnit
            {
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
        public ActionResult Create()
        {
            var allProductTypes = _productTypeRepository.GetAll();

            return View(new CreatePricePlanViewModel {ProductTypes = allProductTypes});
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
        public async Task<ActionResult> Edit(int? id)
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

        // POST: PricePlan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,Name,CustomerId")] PricePlan pricePlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pricePlan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pricePlan);
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
