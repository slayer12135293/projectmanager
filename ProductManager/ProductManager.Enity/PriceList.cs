using System.Collections.Generic;

namespace ProductManager.Enity
{
    public class PriceList : BaseEntity
    {
        public int ProductTypeId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<PriceUnit> PriceUnits { get; set; }
    }
}