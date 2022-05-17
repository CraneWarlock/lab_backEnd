namespace WarehouseManager.Entites
{
    public class Location
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual List<Warehouse> Warehouses { get; set; }
    }
}
