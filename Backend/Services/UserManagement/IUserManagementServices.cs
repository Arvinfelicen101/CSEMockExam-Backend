using Backend.DTOs.Auth;
using Backend.DTOs;
using Backend.DTOs.UserManagement;

namespace Backend.Services.UserManagement;

public interface IUserManagementServices
{
    Task CreateUserAsync(UserManagementCreateDTO dto);
    Task<List<UserManagementListDTO>> GetUsersAsync();
    Task<UserManagementReadDTO> GetUserByIdAsync(string id);
    Task UpdateUserAsync(string id, UserManagementUpdateDTO dto);
    Task DeleteUserAsync(string id);
}