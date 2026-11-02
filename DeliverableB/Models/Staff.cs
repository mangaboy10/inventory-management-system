using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    [Table("Staff")]
    public class Staff
    {
        [Key]
        [Column("Staff_ID")]
        public int Staff_ID { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Staff_Name")]
        public string? Staff_Name { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Department")]
        public string? Department { get; set; }
    }
}