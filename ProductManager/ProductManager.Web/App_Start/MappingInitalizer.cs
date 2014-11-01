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

        }

    }
}