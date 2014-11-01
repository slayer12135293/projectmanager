using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using ProductManager.DataLayer;
using ProductManager.Enity;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Controllers
{
    public class OrderLinesController : Controller
    {
        private CategoryDb db = new CategoryDb();

        public ActionResult Index(int orderId)
        {
            // var ordes = await db.Orders.FindAsync(orderId);
            ViewBag.OrderId = orderId;
            IEnumerable<OrderLine> orderLines = db.OrderLines.Where(x => x.OrderId == orderId).Include("Order").ToList();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<OrderLineViewModel>>(orderLines);
            return View(viewModels);
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