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

namespace ProductManager.Web.Controllers
{
    public class OrderLinesController : Controller
    {
        private OrderDb db = new OrderDb();

        // GET: OrderLines
        public async Task<ActionResult> Index()
        {
            return View(await db.OrderLines.ToListAsync());
        }

        // GET: OrderLines/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLine orderLine = await db.OrderLines.FindAsync(id);
            if (orderLine == null)
            {
                return HttpNotFound();
            }
            return View(orderLine);
        }

        // GET: OrderLines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderLines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ItemPrice,Width,Height,NumberOfItems,UnitDiscount")] OrderLine orderLine)
        {
            if (ModelState.IsValid)
            {
                db.OrderLines.Add(orderLine);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(orderLine);
        }

        // GET: OrderLines/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLine orderLine = await db.OrderLines.FindAsync(id);
            if (orderLine == null)
            {
                return HttpNotFound();
            }
            return View(orderLine);
        }

        // POST: OrderLines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ItemPrice,Width,Height,NumberOfItems,UnitDiscount")] OrderLine orderLine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderLine).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(orderLine);
        }

        // GET: OrderLines/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderLine orderLine = await db.OrderLines.FindAsync(id);
            if (orderLine == null)
            {
                return HttpNotFound();
            }
            return View(orderLine);
        }

        // POST: OrderLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OrderLine orderLine = await db.OrderLines.FindAsync(id);
            db.OrderLines.Remove(orderLine);
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
