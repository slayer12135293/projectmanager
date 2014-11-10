using System.Collections.Generic;

namespace ProductManager.Enity
{
    public class ProductType : BaseEntity
    {
        public string Description { get; set; }
        public virtual ICollection<AddOn> AddOns { get; set; } 
    }
}