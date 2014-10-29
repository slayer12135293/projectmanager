﻿using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProductManager.DataLayer;
using ProductManager.DataLayer.Repositories;
using ProductManager.Enity;
using ProductManager.Web.Services;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserManagerService _userManagerService;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly IProductRepository _productRepository;

        public OrdersController(ICategoryRepository categoryRepository, IUserManagerService userManagerService, ISubCategoryRepository subCategoryRepository, IProductRepository productRepository )
        {
            _categoryRepository = categoryRepository;
            _userManagerService = userManagerService;
            _subCategoryRepository = subCategoryRepository;
            _productRepository = productRepository;
        }
        
        public async Task<ActionResult> AllCategories()
        {
            var currentUser = await _userManagerService.FindByIdAsync(User.Identity.GetUserId());
            var currentCustomerId = currentUser.CustomerId;
            var categories = _categoryRepository.GetAll().Where(x => x.CustomerId == currentCustomerId).Select(y=> new CategoryDropDownViewModel
            {
                Id = y.Id,
                Name = y.Name
            }).ToList();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> SubCategories(int categoryId)
        {
            var subCategories = await _subCategoryRepository.GetSubCategoriesByCategoryId(categoryId);
            var viewModels = subCategories.Select(x => new CategoryDropDownViewModel()
            {
                Id = x.Id,
                Name = x.Name
            });
            return Json(viewModels.ToList(), JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> Products(int subCategoryId)
        {
            var products = await _productRepository.GetProductsFromSubCategory(subCategoryId);
            var viewModels = products.Select(x => new CategoryDropDownViewModel()
            {
                Id = x.Id,
                Name = x.Name
            });
            return Json(viewModels.ToList(), JsonRequestBehavior.AllowGet);
        } 






        private CategoryDb db = new CategoryDb();

        // GET: Orders
        public async Task<ActionResult> Index()
        {
            return View(await db.Orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
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
        public async Task<ActionResult> Create([Bind(Include = "Id,Author,CreatedDate,TotalPrice,Discount,Buyer,Name,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Author,CreatedDate,TotalPrice,Discount,Buyer,Name,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
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
            Order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
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