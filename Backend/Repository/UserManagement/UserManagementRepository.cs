using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.UserManagement
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private readonly UserManager<Users> _manager;

        public UserManagementRepository(UserManager<Users> manager)
        {
            _manager = manager;
        }

        public async Task<IdentityResult> RegisterUserAsync(Users user, string password)
        {
            return await _manager.CreateAsync(user, password);
        }
        public async Task<IdentityResult> CreateUserAsync(Users user, string password)
        {
            return await _manager.CreateAsync(user, password);
        }

        public async Task<Users?> FindByIdAsync(string id)
        {
            return await _manager.FindByIdAsync(id);
        }

        public async Task<Users?> FindEmailAsync(string email)
        {
            return await _manager.FindByEmailAsync(email);
        }

        public async Task<List<Users>> GetAllAsync()
        {
            return await _manager.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<IdentityResult> UpdateUser(Users user)
        {
           return _manager.UpdateAsync(user);
           
        }

        public Task<IdentityResult> DeleteUser(Users user)
        {
            return _manager.DeleteAsync(user);
            
        }

        
    }
}
