using WarehouseManager.Entites;

namespace WarehouseManager.Models
{
    public class WarehouseDto
    {
        public int Id { get; set; }
        public string WarehouseName { get; set; }
        public string Description { get; set; }
        public StorageType StorageType { get; set; }
        public decimal CurrentCapacity { get; set; }
        public decimal MaximumCapacity { get; set; }
        public int LocationId { get; set; }
        
        // TODO: this stuff
        // public virtual List<WarehouseCargoDto> WarehousesCargo { get; set; }

    }
}
