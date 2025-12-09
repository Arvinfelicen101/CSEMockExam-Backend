using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CSEMockInterview.Models
{
  
    public class Users : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; } 
    }
}
