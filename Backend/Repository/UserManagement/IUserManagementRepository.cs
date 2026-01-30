using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Repository.UserManagement;

public interface IUserManagementRepository
{
    Task<IdentityResult> CreateUserAsync(Users user, string password);
}