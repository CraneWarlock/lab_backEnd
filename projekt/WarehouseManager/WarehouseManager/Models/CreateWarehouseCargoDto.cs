using WarehouseManager.Entites;

namespace WarehouseManager.Models
{
    public class CreateWarehouseCargoDto
    {
        public string CargoName { get; set; }
        public CargoType CargoType { get; set; }
        public decimal Volume { get; set; }
        public string Description { get; set; }
        public int WarehouseId { get; set; }
    }
}
