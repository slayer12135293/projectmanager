using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProductManager.DataLayer;
using ProductManager.Enity;
using ProductManager.Web.Services;

namespace ProductManager.Web.Controllers
{
    public class AddOnsController : Controller
    {
        private readonly ICustomerIdService _customerIdService;

        public AddOnsController(ICustomerIdService _customerIdService)
        {
            this._customerIdService = _customerIdService;
        }

        private CategoryDb db = new CategoryDb();

        // GET: AddOns
        public async Task<ActionResult> Index()
        {
            var addOns = db.AddOns.Include(a => a.ProductType);
            return View(await addOns.ToListAsync());
        }

        // GET: AddOns/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddOn addOn = await db.AddOns.FindAsync(id);
            if (addOn == null)
            {
                return HttpNotFound();
            }
            return View(addOn);
        }

        // GET: AddOns/Create
        public ActionResult Create()
        {
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "Id", "Description");
            return View();
        }

        // POST: AddOns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ProductTypeId,Price,Name,CustomerId")] AddOn addOn)
        {
            if (ModelState.IsValid)
            {
                var customerId = await _customerIdService.GetCustomerId();
                addOn.CustomerId = customerId;
                db.AddOns.Add(addOn);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "Id", "Description", addOn.ProductTypeId);
            return View(addOn);
        }

        // GET: AddOns/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddOn addOn = await db.AddOns.FindAsync(id);
            if (addOn == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "Id", "Description", addOn.ProductTypeId);
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
                db.Entry(addOn).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductTypeId = new SelectList(db.ProductTypes, "Id", "Description", addOn.ProductTypeId);
            return View(addOn);
        }

        // GET: AddOns/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddOn addOn = await db.AddOns.FindAsync(id);
            if (addOn == null)
            {
                return HttpNotFound();
            }
            return View(addOn);
        }

        // POST: AddOns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AddOn addOn = await db.AddOns.FindAsync(id);
            db.AddOns.Remove(addOn);
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
