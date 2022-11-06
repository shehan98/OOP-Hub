namespace EmpiteIMS.Models.Inventory
{
    public class GetInventoryViewModel
    {
        public int ItemCode { get; set; }

        public string ItemName { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
