using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Repository.UserManagement;

public interface IUserManagementRepository
{
    Task<IdentityResult> CreateUserAsync(Users user, string password);
    Task<Users?> FindByIdAsync(string id);
    Task<Users?> FindEmailAsync(string email);
    Task<List<Users>> GetAllAsync();
    Task UpdateUser(Users user);
    Task DeleteUser(Users user);
}
