
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Enity
{
    public class Category : BaseEntity
    {
        public string Description { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
   
}
