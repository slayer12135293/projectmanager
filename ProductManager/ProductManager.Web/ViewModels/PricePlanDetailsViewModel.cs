using System.Collections.Generic;
using ProductManager.Enity;

namespace ProductManager.Web.ViewModels
{
    public class PricePlanDetailsViewModel
    {
        public PriceList PriceList { get; set;}
        public PriceUnitViewModel PriceUnitViewModel { get; set; }
        public IEnumerable<PriceUnitViewModel> PriceUnitViewModels { get; set; } 
    }           
}