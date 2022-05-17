namespace WarehouseManager.Entites
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string WarehouseName { get; set; }
        public string Description { get; set; }
        public StorageType StorageType { get; set; }
        public decimal CurrentCapacity { get; set; }
        public decimal MaximumCapacity { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public virtual List<WarehouseCargo> WarehousesCargo { get; set; }
    }

    public enum StorageType
    {
        Hall,
        Silo
    }
}

