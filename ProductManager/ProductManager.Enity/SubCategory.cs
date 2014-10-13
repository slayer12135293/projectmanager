using System.Collections.Generic;

namespace ProductManager.Enity
{
    public class SubCategory : BaseEntity
    {
        public string Descrition { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
