using System.Collections.Generic;

namespace ProductManager.Web.ViewModels
{
    public class ProductTypeGroupViewModel
    {
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeDescription { get; set; }
        public virtual ICollection<OrderLineViewModel> OrderLines { get; set; }
        public virtual ICollection<AddOnViewModel> AddOns { get; set; } 
    }
}