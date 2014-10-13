namespace ProductManager.Enity
{
    public class Company : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int PostCode { get; set; }
        public string City { get; set; }
        public string ContactPerson { get; set; }

    }


}
