using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProductManager.Enity;
using Microsoft.AspNet.Identity.Owin;
using ProductManager.Web.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ProductManager.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ProductManager.Web.App_Start.NinjectWebCommon), "Stop")]

namespace ProductManager.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using DataLayer.Repositories;
    using Factories;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            kernel.Bind<IProductCategoryViewModelFactory>().To<ProductCatagoryViewModelFactory>();
            kernel.Bind<IProductCategoryDetailViewModelFactory>().To<ProductCategoryDetailViewModelFactory>();
            kernel.Bind<IProductSubCategoryViewModelFactory>().To<ProductSubCategoryViewModelFactory>();
            kernel.Bind<ISubCategoryRepository>().To<SubCategoryRepository>();
            kernel.Bind<IProductRepository>().To<ProductRepository>().InRequestScope();
            kernel.Bind<IUserManagerService>().ToMethod(x => HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationUserManager>());
            kernel.Bind<IApplicationRoleManager>().ToMethod(x => HttpContext.Current.Request.GetOwinContext().GetUserManager<ApplicationRoleManager>());
            kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>();
            kernel.Bind<ICustomerIdService>().To<CustomerIdService>();
            kernel.Bind<IOrderRepository>().To<OrderRepository>();
            kernel.Bind<IAddOnRepository>().To<AddOnRepository>();
            kernel.Bind<IProductTypeRepository>().To<ProductTypeRepository>();
            kernel.Bind<IProductCreateViewModelFactory>().To<ProductCreateViewModelFactory>();
            kernel.Bind<IUpdateViewModelProductFacotry>().To<UpdateViewModelProductFacotry>();
            kernel.Bind<IPricePlanRepository>().To<PricePlanRepository>();
            kernel.Bind<IPriceUnitRepository>().To<PriceUnitRepository>();
            kernel.Bind<IPricePlanViewModelFactory>().To<PricePlanViewModelFactory>();
            kernel.Bind<ICustomerRepository>().To<CustomerRepository>();
            kernel.Bind<IPriceListPriceService>().To<PriceListPriceService>();
            kernel.Bind<IPricePlanService>().To<PricePlanService>();
            kernel.Bind<IPriceUnitService>().To<PriceUnitService>();

        }        
    }
}
