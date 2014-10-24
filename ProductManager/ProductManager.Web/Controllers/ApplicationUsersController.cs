using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using ProductManager.DataLayer;
using ProductManager.Enity;
using ProductManager.Web.Models;
using ProductManager.Web.Services;

namespace ProductManager.Web.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly IUserManagerService _applicationUserManager;
        private readonly IApplicationRoleManager _applicationRoleManager;

        public ApplicationUsersController(IUserManagerService applicationUserManager, IApplicationRoleManager applicationRoleManager)
        {
            _applicationUserManager = applicationUserManager;
            _applicationRoleManager = applicationRoleManager;
        }


        public DbSet<ApplicationUser> ApplicationUsers
        {
            get { return (DbSet<ApplicationUser>)db.Users; }
        }

        private CategoryDb db = new CategoryDb();

        // GET: ApplicationUsers
        public async Task<ActionResult> Index()
        {
            var applicationUsers = ApplicationUsers.Include(a => a.Customer);
            return View(await applicationUsers.ToListAsync());
        }

        // GET: ApplicationUsers/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await ApplicationUsers.FindAsync(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");

            //var userRoles = await _applicationUserManager.GetRolesAsync(user.Id);

            //ViewBag.RolesList = _applicationRoleManager.Roles.ToList().Select(x => new SelectListItem()
            //{
            //    Text = x.Name,
            //    Value = x.Name
            //});

            ViewBag.RoleId = new SelectList(await _applicationRoleManager.Roles.ToListAsync(), "Name", "Name");


            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel applicationUser, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                //ApplicationUsers.Add(applicationUser);
                //await db.SaveChangesAsync();

                var user = new ApplicationUser() { UserName = applicationUser.Email, Email = applicationUser.Email, CustomerId = applicationUser.CustomerId };
                //  user.Claims.Add(new IdentityUserClaim() {ClaimType = ClaimTypes.});
                IdentityResult result = await _applicationUserManager.CreateAsync(user, applicationUser.Password);

                if (result.Succeeded)
                {
                    var addToRoleResult = await _applicationUserManager.AddToRolesAsync(user.Id, selectedRoles.ToArray<string>());
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }

            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", applicationUser.CustomerId);
            return View(applicationUser);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // GET: ApplicationUsers/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await ApplicationUsers.FindAsync(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", applicationUser.CustomerId);

            RegisterViewModel viewModel = new RegisterViewModel();
            viewModel.Id = id;
            viewModel.Email = applicationUser.Email;
            
            var userRoles = await _applicationUserManager.GetRolesAsync(id);

            viewModel.RolesList = _applicationRoleManager.Roles.ToList().Select(x => new SelectListItem()
            {
                Selected = userRoles.Contains(x.Name),
                Text = x.Name,
                Value = x.Name
            });

            return View(viewModel);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CustomerId,IsActive,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", applicationUser.CustomerId);
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await ApplicationUsers.FindAsync(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = await ApplicationUsers.FindAsync(id);
            ApplicationUsers.Remove(applicationUser);
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
