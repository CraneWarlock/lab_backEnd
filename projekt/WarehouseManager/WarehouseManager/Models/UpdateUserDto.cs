using System.ComponentModel.DataAnnotations;

namespace WarehouseManager.Models
{
    public class UpdateUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Department { get; set; }
        public int RoleId { get; set; }
    }
}
