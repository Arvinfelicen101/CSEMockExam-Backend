using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.UserManagement
{
    public class UserManagementUpdateDTO
    {
        [Required]
        public string FirstName { get; set; } = default!;

        [Required]
        public string LastName { get; set; } = default!;

        public string? MiddleName { get; set; }
    }
}
