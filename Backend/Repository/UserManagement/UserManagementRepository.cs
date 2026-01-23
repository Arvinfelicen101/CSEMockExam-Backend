using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Repository.UserManagement
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private readonly UserManager<Users> _manager;

        public UserManagementRepository(UserManager<Users> manager)
        {
            _manager = manager;
        }
        public async Task<IdentityResult> CreateUserAsync(Users user, string password)
        {
            return await _manager.CreateAsync(user, password);
        }

        public async Task<Users?> FindByIdAsync(string id)
        {
           return await _manager.FindByIdAsync(id);
        }

        public async Task<List<Users>> GetAllAsync()
        {
            return _manager.Users.ToList();
        }

        public async Task UpdateAsync(Users user)
        {
            await _manager.UpdateAsync(user);
        }

        public async Task DeleteAsync(Users user)
        {
            await _manager.DeleteAsync(user);
        }
        
    }
}
