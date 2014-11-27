namespace ProductManager.Enity
{
    public interface IPricePlanPriceService
    {
        int? GetPrice(PricePlan pricePlan, int height, int width);
    }
}