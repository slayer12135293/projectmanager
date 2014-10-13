using System.ComponentModel.DataAnnotations;

namespace ProductManager.Enity
{
    public abstract class BaseEntity {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } 
    }
}