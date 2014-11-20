using System.Collections.Generic;

namespace ProductManager.Enity
{
    public class Buyer : BaseEntity
    {
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Mobil { get; set; }
        public string Information { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}