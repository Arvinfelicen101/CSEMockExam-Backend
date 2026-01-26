using Backend.DTOs.Auth;
using Backend.DTOs;
using Backend.DTOs.UserManagement;
using Backend.Models;

namespace Backend.Services.UserManagement;

public interface IUserManagementServices
{
    Task CreateUserAsync(UserManagementCreateDTO dto);
    Task<List<UserManagementListDTO>> GetUsersAsync();
    Task UpdateUserAsync(string id, UserManagementUpdateDTO dto);
    Task DeleteUserAsync(string id);
}