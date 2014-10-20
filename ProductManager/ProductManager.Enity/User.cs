using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManager.Enity
{
    public class User : BaseEntity
    {
        [Index]
        [MaxLength(256)]
        public virtual Customer Customer { get; set; }
        public string UserName { get; set; }

    }
}