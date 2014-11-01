using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProductManager.Enity
{
    public class Order : BaseEntity
    {
       

        [Required]
        [Index]
        [MaxLength(256)]
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<OrderLine> Products { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public Buyer Buyer { get; set; }

    }

    [ComplexType]
    public class Buyer
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Mobil { get; set; }
        public string Information { get; set; }
    }


}
