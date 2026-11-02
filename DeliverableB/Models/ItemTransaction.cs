using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    [Table("Item_Transaction")]
    public class ItemTransaction
    {
        [Key]
        [Column("Transaction_ID")]
        public int Transaction_ID { get; set; }

        [Required]
        [Column("Item_ID")]
        public int Item_ID { get; set; }

        [ForeignKey("Item_ID")]
        public Item? Item { get; set; }

        [Required]
        [StringLength(1)]
        [Column("Transaction_Type")]
        public string? Transaction_Type { get; set; }

        [Required]
        [Column("Qty")]
        public int Qty { get; set; }

        [Column("Supplier_ID")]
        public int? Supplier_ID { get; set; }

        [ForeignKey("Supplier_ID")]
        public Supplier? Supplier { get; set; }

        [Column("Staff_ID")]
        public int? Staff_ID { get; set; }

        [ForeignKey("Staff_ID")]
        public Staff? Staff { get; set; }

        [Required]
        [Column("Transaction_Date")]
        public DateTime Transaction_Date { get; set; }

        [Column("Remarks")]
        public string? Remarks { get; set; }
    }
}