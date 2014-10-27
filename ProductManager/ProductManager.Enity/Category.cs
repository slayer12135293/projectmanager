using System.Collections.Generic;

namespace ProductManager.Enity
{
    public class Category : BaseEntity
    {
        public string Description { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
        
    }
   
}
