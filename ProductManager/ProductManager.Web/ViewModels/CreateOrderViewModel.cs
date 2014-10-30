using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProductManager.Enity;

namespace ProductManager.Web.ViewModels
{
    public class CreateOrderViewModel
    {
        [Required]
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<OrderLine> Products { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class OrderLineViewModel
    {

    }
}