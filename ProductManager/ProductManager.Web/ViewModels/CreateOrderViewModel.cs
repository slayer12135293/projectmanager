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
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public virtual ICollection<ProductTypeGroupViewModel> ProductTypeGroups { get; set;}
        public virtual ICollection<AddOnViewModel> AddOns { get; set; } 
        public decimal TotalPrice { get; set; }
        public Buyer Buyer { get; set; }
        public string AdditionalInformation { get; set; }
    }
}