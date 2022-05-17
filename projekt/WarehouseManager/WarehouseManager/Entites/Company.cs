namespace WarehouseManager.Entites
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public virtual List<Location> Locations { get; set; }
    }
}
