using System.ComponentModel.DataAnnotations;

namespace WarehouseManager.Models
{
    public class UpdateLocationDto
    {
        [Required]
        [MaxLength(30)]
        public string LocationName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }

        // public int CompanyId { get; set; }
    }
}
