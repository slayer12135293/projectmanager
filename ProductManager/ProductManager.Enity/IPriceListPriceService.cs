namespace ProductManager.Enity
{
    public interface IPriceListPriceService
    {
        decimal? GetPrice(PriceList priceList, int height, int width);
    }
}