
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Enity
{
    public class SubCategory : BaseEntity
    {
        public string Descrition { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
