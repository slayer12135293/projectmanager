using System.Collections.Generic;

namespace ProductManager.Enity
{
    public class ProductType : BaseEntity
    {
        public string Description { get; set; }
        public virtual ICollection<AddOn> AddOns { get; set; }
        public PriceCalculationType PriceCalculationType { get; set; }

    }

    public enum PriceCalculationType
    {
        None = 0,
        WithHeightAmount = 1,
        SquareMetersAmount = 2,
        Amount = 3,
    }
}