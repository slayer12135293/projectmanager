using Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.DataLayer
{
    public class OrderDb : DbContext
    {
        public OrderDb()
            : base("ProductManagerConnection")
        {
        }
        public DbSet<Order> Orders { get;set; }

        public System.Data.Entity.DbSet<Service.Entities.OrderLine> OrderLines { get; set; }
    }
}
