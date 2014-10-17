using AutoMapper;
using ProductManager.Enity;

namespace ProductManager.DataLayer.Repositories
{
    public static class MappingInitalizer
    {
        public static void CreateMaps()
        {
            Mapper.CreateMap<Product, Product>();
        }
    }
}