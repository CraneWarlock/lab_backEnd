namespace WarehouseManager.Models
{
    public class UpdateWarehouseDto
    {
        public string WarehouseName { get; set; }
        public string Description { get; set; }

       // public decimal CurrentCapacity { get; set; }
        public decimal MaximumCapacity { get; set; }
    }
}
