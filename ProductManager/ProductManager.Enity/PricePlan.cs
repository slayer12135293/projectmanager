using System.Collections.Generic;
using System.Linq;

namespace ProductManager.Enity
{
    public class PricePlan : BaseEntity
    {
        public int ProductTypeId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<PriceUnit> PriceUnits { get; set; }

        public int? GetPrice(int height, int width)
        {
            if (PriceUnits == null)
                return null;

            var priceUnit = PriceUnits.FirstOrDefault(x => x.Width == width - width % 10 && x.Height == height - height % 10);
            if (priceUnit == null)
            {
                return null;
            }
            return priceUnit.Price;
        }
    }
}