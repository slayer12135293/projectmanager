using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Enity
{
    public abstract class BaseEntity {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } 
    }

    public class ProductCatagory :BaseEntity
    {
        public ProductCatagory ParentCategory { get; set; }
        public string Description { get; set; }
    }

    public class Company : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int PostCode { get; set; }
        public string City { get; set; }
        public string ContactPerson { get; set; }

    }


}
