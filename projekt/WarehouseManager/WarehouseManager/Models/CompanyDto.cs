namespace WarehouseManager.Models
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public List<LocationDto> Locations { get; set; }
    }
}
