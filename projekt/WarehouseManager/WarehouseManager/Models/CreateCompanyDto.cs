using System.ComponentModel.DataAnnotations;

namespace WarehouseManager.Models
{
    public class CreateCompanyDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }
    }
}
