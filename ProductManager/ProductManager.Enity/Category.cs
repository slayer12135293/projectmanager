using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManager.Enity
{
    public class Category : BaseEntity
    {
        public string Description { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
        
    }
   
}
