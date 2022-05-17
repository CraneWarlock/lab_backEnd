namespace WarehouseManager.Entites
{
    public class WarehouseCargo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CargoType CargoType { get; set; }
        public decimal Volume { get; set; }
        public string Description { get; set; }
        public int WarehouseId { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }

    public enum CargoType
    {
        Palette,
        Crate,
        Liquid,
        BulkMaterial
    }
}
