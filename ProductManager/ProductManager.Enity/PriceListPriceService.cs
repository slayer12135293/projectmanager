using System.Linq;

namespace ProductManager.Enity
{
    public class PriceListPriceService : IPriceListPriceService
    {
        private static int ToClosestTenth(int value)
        {
            return (value - value % 10);
        }

        public decimal? GetPrice(PriceList priceList, int height, int width)
        {
            if (priceList.PriceUnits == null)
                return null;

            var priceUnit = priceList.PriceUnits.FirstOrDefault(x => x.Width == ToClosestTenth(width) + 10 && x.Height == ToClosestTenth(height) + 10);
            if (priceUnit == null)
            {
                return null;
            }
            return priceUnit.Price;
        }
    }
}