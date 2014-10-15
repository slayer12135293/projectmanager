using System.Collections.Generic;
using System.Linq;

namespace ProductManager.Enity
{
    public class SubCategory : BaseEntity
    {
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
