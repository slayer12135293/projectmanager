using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProductManager.DataLayer;
using ProductManager.Enity;
using ProductManager.Web.Services;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Controllers
{
    public class OrderLinesController : Controller
    {
        private readonly IUserManagerService _userManagerService;
        private readonly CategoryDb _db = new CategoryDb();

        public OrderLinesController(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }

        public ActionResult Index(int orderId)
        {
            ViewBag.OrderId = orderId;
            IEnumerable<OrderLine> orderLines = _db.OrderLines.Where(x => x.OrderId == orderId).Include("Order").ToList();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<OrderLineViewModel>>(orderLines);
            return View(viewModels);
        }

        public async Task<ActionResult> Create(int orderId)
        {
            ViewBag.OrderId = orderId;
            var currentUser = await _userManagerService.FindByIdAsync(User.Identity.GetUserId());
            var currentCustomerId = currentUser.CustomerId;
            ViewBag.OrderId = orderId;
            ViewBag.ProductId = new SelectList(_db.Products.Where(x => x.CustomerId == currentCustomerId), "CustomerId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateOrderLineViewModel createOrderLineViewModel)
        {
            var currentUser = await _userManagerService.FindByIdAsync(User.Identity.GetUserId());
            var currentCustomerId = currentUser.CustomerId;

            ViewBag.ProductId = new SelectList(_db.Products.Where(x => x.CustomerId == currentCustomerId), "CustomerId", "Name");
            var product = await _db.Products.FindAsync(createOrderLineViewModel.ProductId);

            var newOrderLine = new OrderLine
            {
                Height = createOrderLineViewModel.Height,
                Width = createOrderLineViewModel.Width,
                ItemPrice = createOrderLineViewModel.ItemPrice,
                NumberOfItems = 1,
                ProductName = product.Name,
                OrderId = createOrderLineViewModel.OrderId,
                ProductId = createOrderLineViewModel.ProductId
            };

            _db.OrderLines.Add(newOrderLine);
            await _db.SaveChangesAsync();
            return RedirectToAction("Details", new { controller = "Orders", Id = createOrderLineViewModel.OrderId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}