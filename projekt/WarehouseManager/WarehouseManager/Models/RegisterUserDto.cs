using System.ComponentModel.DataAnnotations;

namespace WarehouseManager.Models
{
    public class RegisterUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        
        public string Email{ get; set; }
        
        [MinLength(6)]
        public string Password{ get; set; }
        public string ConfirmPassword { get; set; }
        public string? Department { get; set; }
        public int RoleId { get; set; } = 1;
    }
}
