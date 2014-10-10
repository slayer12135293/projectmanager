using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Entities
{
    //public interface ICommand
    //{
    //    void Execute();
    //}

    //public class CreateOrderCommand : ICommand
    //{

    //    public void Execute()
    //    { 
            
    //    }
    //}

    public class Order
    {
        public int OrderID { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<OrderLine> Products { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }

    }

    public class OrderLine 
    {
        public int Id { get; set; }
        public virtual Order Order {get;set;}
        public decimal ItemPrice { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public int NumberOfItems { get; set; }
        public Product Product { get; set; }
        public decimal UnitDiscount { get; set; }
    }
}
