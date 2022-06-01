using WarehouseManager.Entites;
using System.ComponentModel.DataAnnotations;

namespace WarehouseManager.Models
{
    public class CreateWarehouseDto
    {
        [Required]
        [MaxLength(30)]
        public string WarehouseName { get; set; }
        public string Description { get; set; }

        [Required]
        public StorageType StorageType { get; set; }

        [Required]
        public decimal CurrentCapacity { get; set; }

        [Required]
        public decimal MaximumCapacity { get; set; }

        [Required]
        public int LocationId { get; set; }

    }
}
