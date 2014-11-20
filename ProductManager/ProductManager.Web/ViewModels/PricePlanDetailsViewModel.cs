using System.Collections.Generic;
using ProductManager.Enity;

namespace ProductManager.Web.ViewModels
{
    public class PricePlanDetailsViewModel
    {
        public PricePlan PricePlan { get; set;}
        public PriceUnitViewModel PriceUnitViewModel { get; set; }
        public IEnumerable<PriceUnitViewModel> PriceUnitViewModels { get; set; } 
    }

    public class PricePlanDropDownViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}