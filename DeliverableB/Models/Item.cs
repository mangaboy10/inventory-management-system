using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    [Table("Item")]
    public class Item
    {
        [Key]
        [Column("Item_ID")]
        public int Item_ID { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Item_Name")]
        public string? Item_Name { get; set; } = "";

        [Required]
        [StringLength(20)]
        [Column("Unit")]
        public string? Unit { get; set; } = "";

        [Required]
        [Column("Qty_On_Hand")]
        public int Qty_On_Hand { get; set; }

        [Required]
        [Column("Reorder_Level")]
        public int Reorder_Level { get; set; }
    }
}