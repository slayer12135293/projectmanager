using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities
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

}
