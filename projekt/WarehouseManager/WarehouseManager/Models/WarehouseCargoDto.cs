using System.Text.Json.Serialization;
using WarehouseManager.Entites;

namespace WarehouseManager.Models
{
    public class WarehouseCargoDto
    {
        public int Id { get; set; }
        public string CargoName { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CargoType CargoType { get; set; }
        public decimal Volume { get; set; }
        public string Description { get; set; }
        public int WarehouseId { get; set; }
    }
}
