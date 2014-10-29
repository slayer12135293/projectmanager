using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManager.Enity
{
    public abstract class BaseEntity {
        public int Id { get; set; }
        [Required]
        [Index]
        [MaxLength(256)]
        public string Name { get; set; }
       
        [Index]
        public int CustomerId { get; set; }


    }
}