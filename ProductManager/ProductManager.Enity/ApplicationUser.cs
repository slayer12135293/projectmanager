using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProductManager.Enity
{
    public class ApplicationUser : IdentityUser
    {
       
        [Index]
        [MaxLength(256)]
        public virtual Customer Customer { get; set; }
        [Index]
        public int CustomerId { get; set; }
        public bool IsActive { get; set; }
    }
}