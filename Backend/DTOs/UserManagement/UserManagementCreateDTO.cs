using Backend.DTOs.Exams;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.UserManagement
{
    public class UserManagementCreateDTO
    {
        [Required(ErrorMessage = "First Name is required.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string? LastName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
