using WarehouseManager.Entites;
using System.Text.Json.Serialization;

namespace WarehouseManager.Models
{
    public class WarehouseDto
    {
        public int Id { get; set; }
        public string WarehouseName { get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StorageType StorageType { get; set; }
        public decimal CurrentCapacity { get; set; }
        public decimal MaximumCapacity { get; set; }
        public int LocationId { get; set; }
        
        public virtual List<WarehouseCargoDto> WarehousesCargo { get; set; }

    }
}
