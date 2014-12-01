using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.Filters;
using ProductManager.Web.Services;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Controllers
{

    [AdministratorFilter]
    public class OrdersController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserManagerService _userManagerService;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IAddOnRepository _addOnRepository;
        private readonly IPricePlanRepository _pricePlanRepository;
        private readonly IPricePlanPriceService _pricePlanPriceService;

        public OrdersController(ICategoryRepository categoryRepository,
            IUserManagerService userManagerService,
            ISubCategoryRepository subCategoryRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IAddOnRepository addOnRepository,
            IPricePlanRepository pricePlanRepository,
            IPricePlanPriceService pricePlanPriceService
            )
        {
            _categoryRepository = categoryRepository;
            _userManagerService = userManagerService;
            _subCategoryRepository = subCategoryRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _addOnRepository = addOnRepository;
            _pricePlanRepository = pricePlanRepository;
            _pricePlanPriceService = pricePlanPriceService;
        }

        public async Task<ActionResult> AllCategories()
        {
            var currentUser = await _userManagerService.FindByIdAsync(User.Identity.GetUserId());
            var currentCustomerId = currentUser.CustomerId;
            var categories = _categoryRepository.GetAll().OrderBy(o => o.Name).Where(x => x.CustomerId == currentCustomerId).Select(y => new CategoryDropDownViewModel
            {
                Id = y.Id,
                Name = y.Name
            }).ToList();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SubCategories(int categoryId)
        {
            var subCategories = await _subCategoryRepository.GetSubCategoriesByCategoryId(categoryId);
            var viewModels = subCategories.OrderBy(o => o.Name).Select(x => new CategoryDropDownViewModel()
            {
                Id = x.Id,
                Name = x.Name
            });
            return Json(viewModels.ToList(), JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> Products(int subCategoryId, int productTypeId)
        {
            var products = await _productRepository.GetProductsFromSubCategory(subCategoryId);
            var viewModels = products.Where(t => t.ProductTypeId == productTypeId).OrderBy(o => o.Name).Select(x => new CategoryDropDownViewModel()
            {
                Id = x.Id,
                Name = x.Name
            });
            return Json(viewModels.ToList(), JsonRequestBehavior.AllowGet);
        }

        //TODO change this to GetPrice
        //public async Task<ActionResult> GetProductById(int productId, int width, int height)
        //{
        //    var product = await _productRepository.GetByIdAsync(productId);
        //    var viewModel = AutoMapper.Mapper.Map<ProductViewModel>(product);
        //    var pricePlan = await _pricePlanRepository.GetByIdAsync(product.PricePlanId);
        //    var price = pricePlan.GetPrice(height, width);
        //    viewModel.UnitPrice = price.GetValueOrDefault(0);
        //    return Json(viewModel, JsonRequestBehavior.AllowGet);
        //}


        public async Task<ActionResult> GetProductById(int productId, int productTypeId,int width, int height)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            var viewModel = AutoMapper.Mapper.Map<ProductViewModel>(product);
            var pricePlan = await _pricePlanRepository.GetByIdAsync(product.PricePlanId);
            if (pricePlan == null){
                return Json(viewModel, JsonRequestBehavior.AllowGet);
            }

            var price = _pricePlanPriceService.GetPrice(pricePlan, height, width);
            viewModel.UnitPrice = price.GetValueOrDefault(0);
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }



        public async Task<ActionResult> GetAddOnsByProductType(int productTypeId)
        {
            var result = await _addOnRepository.GetAddOnsByProductType(productTypeId);
            var viewModel = AutoMapper.Mapper.Map<IEnumerable<AddOnViewModel>>(result);
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        // GET: Orders
        public async Task<ActionResult> Index()
        {
            return View(await _orderRepository.GetAll().ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Order order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(AutoMapper.Mapper.Map<OrderDetailsViewModel>(order));
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateOrderViewModel order)
        {
            //TODO update this to new order create model
            //if (ModelState.IsValid)
            //{
            //    var currentUser = await _userManagerService.FindByIdAsync(User.Identity.GetUserId());
            //    var currentCustomerId = currentUser.CustomerId;

            //    var orderToSave = new Order();
            //    orderToSave.Name = "my order";
            //    orderToSave.Buyer = new Buyer() { Address = order.Buyer.Address, Information = order.Buyer.Information, Mobil = order.Buyer.Mobil, Name = order.Buyer.Mobil, Telephone = order.Buyer.Telephone };
            //    orderToSave.Products = new Collection<OrderLine>();
            //    orderToSave.Author = order.Author;
            //    orderToSave.CreatedDate = DateTime.UtcNow;
            //    orderToSave.TotalPrice = order.TotalPrice;
            //    orderToSave.CustomerId = currentCustomerId;
            //    orderToSave.Discount = 0;

            //    foreach (var typeGroup in order.ProductTypeGroups)
            //    {


            //        var line = new OrderLine();
            //        line.ProductId = prod.Id;
            //        var product = await _productRepository.GetByIdAsync(prod.Id);
            //        line.ProductName = product.Name;
            //        line.Height = prod.Height;
            //        line.Width = prod.Width;
            //        line.NumberOfItems = 1;
            //        line.UnitDiscount = 0;
            //        line.ItemPrice = prod.ItemPrice;
            //        orderToSave.Products.Add(line);
            //    }

            //    await _orderRepository.Add(orderToSave);

            //    return RedirectToAction("Index");
            //}

            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Order order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "Id,Author,CreatedDate,TotalPrice,Discount,Buyer,Name,CustomerId")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(order).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(order);
        //}

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Order order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Order order = await _orderRepository.GetByIdAsync(id);
            await _orderRepository.RemoveAsync(order);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> OrderLines(int orderId)
        {
            // var ordes = await db.Orders.FindAsync(orderId);
            var order = await _orderRepository.GetByIdAsync(orderId);
            var orderLines = order.Products.OrderBy(x => x.ProductName).ToList();
            var viewModels = AutoMapper.Mapper.Map<IEnumerable<OrderLineViewModel>>(orderLines);
            return View(viewModels);
        }

        public async Task<ActionResult> PriceForOrderLine(int height, int width, int pricePlanId)
        {
            var product = await _pricePlanRepository.GetByIdAsync(pricePlanId);
            return Json(_pricePlanPriceService.GetPrice(product,height, width));
        }
    }
}
