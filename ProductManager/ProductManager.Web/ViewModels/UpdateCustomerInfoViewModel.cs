using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManager.Web.ViewModels
{
    public class UpdateCustomerInfoViewModel
    {
        [Required]
        [Index]
        [MaxLength(256)]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        [Required]
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        [Required]
        public string KeyContact { get; set; }
        [Required]
        public string Email { get; set; }
    }
}