using System;
using System.Collections.Generic;


namespace ProductManager.Enity
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<OrderLine> Products { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }

    }

}
