namespace WarehouseManager.Entites
{
    public class Location
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode{ get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual List<Warehouse> Warehouses { get; set; }
    }
}
