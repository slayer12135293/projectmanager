using System.Collections.Generic;
using System.ComponentModel;

namespace ProductManager.Enity
{
    public class PricePlan : BaseEntity
    {
        public int ProductTypeId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<PriceUnit> PriceUnits { get; set; } 
    }

    public class PricePlanDetailsViewModel
    {
        public PricePlan PricePlan { get; set;}
        public PriceUnitViewModel PriceUnitViewModel { get; set; }
        public IEnumerable<PriceUnitViewModel> PriceUnitViewModels { get; set; } 
        
    }

    public class PriceUnitViewModel
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Price { get; set; }
    }
}