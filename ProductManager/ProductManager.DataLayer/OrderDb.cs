﻿using ProductManager.Enity;
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

        public DbSet<OrderLine> OrderLines { get; set; }

        public DbSet<ProductCatagory> ProductCatagories { get; set; }
    }
}
