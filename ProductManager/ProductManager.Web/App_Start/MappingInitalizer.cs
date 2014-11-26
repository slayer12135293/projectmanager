using AutoMapper;
using ProductManager.Enity;
using ProductManager.Web.ViewModels;

namespace ProductManager.Web
{
    public static class MappingInitalizer
    {
        public static void CreateMaps()
        {
            Mapper.CreateMap<Product, Product>();
            Mapper.CreateMap<Product, ProductViewModel>();
            Mapper.CreateMap<Order, OrderDetailsViewModel>();
            Mapper.CreateMap<OrderLine, OrderLineViewModel>();
            Mapper.CreateMap<AddOn, AddOnViewModel>();
            Mapper.CreateMap<Product, CreateProductViewModel>().ForMember(x=>x.ProductTypeId, opt=> opt.Ignore());
            Mapper.CreateMap<CreateProductViewModel, Product>();
            Mapper.CreateMap<Product, EditProductViewModel>();
            Mapper.CreateMap<CreatePricePlanViewModel, PricePlan>().ForMember(x => x.PriceUnits, opt => opt.Ignore());
            Mapper.CreateMap<Customer, UpdateCustomerInfoViewModel>();
            Mapper.CreateMap<UpdateCustomerInfoViewModel, Customer>();

        }

    }
}