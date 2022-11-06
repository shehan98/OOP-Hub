using System.ComponentModel.DataAnnotations;

namespace EmpiteIMS.Models.Inventory
{
    public class InventoryModel
    {
        [Key]
        [Required]
        public int ItemCode { get; set; }

        [Required]
        public string ItemName { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }
    }
}
