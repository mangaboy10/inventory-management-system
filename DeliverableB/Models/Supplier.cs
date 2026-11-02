using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    [Table("Supplier")]
    public class Supplier
    {
        [Key]
        [Column("Supplier_ID")]
        public int Supplier_ID { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Supplier_Name")]
        public string? Supplier_Name { get; set; } = "";
    }
}