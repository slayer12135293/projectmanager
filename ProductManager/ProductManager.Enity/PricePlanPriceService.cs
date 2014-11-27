using System.Linq;

namespace ProductManager.Enity
{
    public class PricePlanPriceService : IPricePlanPriceService
    {
        private static int ToClosestTenth(int value)
        {
            return (value - value % 10);
        }

        public int? GetPrice(PricePlan pricePlan, int height, int width)
        {
            if (pricePlan.PriceUnits == null)
                return null;

            var priceUnit = pricePlan.PriceUnits.FirstOrDefault(x => x.Width == ToClosestTenth(width) + 10 && x.Height == ToClosestTenth(height) + 10);
            if (priceUnit == null)
            {
                return null;
            }
            return priceUnit.Price;
        }
    }
}