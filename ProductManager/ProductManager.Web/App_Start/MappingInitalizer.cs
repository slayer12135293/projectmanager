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
            Mapper.CreateMap<Product, CreateProductViewModel>().ForMember(x=>x.ProductType, opt=> opt.Ignore());
            Mapper.CreateMap<CreateProductViewModel, Product>().ForMember(x=>x.ProductType, opt=> opt.Ignore());

        }

    }
}