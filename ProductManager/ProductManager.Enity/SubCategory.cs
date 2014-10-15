using System.Collections.Generic;

namespace ProductManager.Enity
{
    public class SubCategory : BaseEntity
    {
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
