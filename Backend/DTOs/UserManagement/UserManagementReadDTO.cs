using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.UserManagement
{
    public class UserManagementReadDTO
    {
        public string Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string MiddleName { get; set; } = default!;
    }
}
