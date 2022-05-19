using System.ComponentModel.DataAnnotations;

namespace WarehouseManager.Models
{
    public class CreateLocationDto
    {
        [Required]
        [MaxLength(30)]
        public string LocationName { get; set; }
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}
