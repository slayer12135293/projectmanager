using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManager.Enity
{
    public class Customer 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        [Required]
        [Index]
        [MaxLength(256)]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string KeyContact { get; set; }
        public string Email { get; set; }
        public string Bankgiro { get; set; }
        public string Postgiro { get; set; }
        public string OrganizationNumber { get; set; }
        public string AdditionalInformation { get; set; }
        public bool IsActive { get; set; }

    }
}