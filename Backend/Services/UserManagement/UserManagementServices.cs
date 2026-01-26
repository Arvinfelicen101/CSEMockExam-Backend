using Backend.DTOs.Auth;
using Backend.DTOs.UserManagement;
using Backend.Exceptions;
using Backend.Models;
using Backend.Repository.UserManagement;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Services.UserManagement
{
    public class UserManagementServices : IUserManagementServices
    {
        private readonly IUserManagementRepository _repository;
        private readonly IMemoryCache _cache;
        
        public UserManagementServices(IUserManagementRepository repository, IMemoryCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public  async Task CreateUserAsync(UserManagementCreateDTO dto)
        {
            var emailExist = await _repository.FindEmailAsync(dto.email);
            if (emailExist != null) 
                throw new BadRequestException("Email already exists");

            var userInfo = new Users
            {
                Email = dto.email,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,

            };

            await _repository.CreateUserAsync(userInfo, dto.Password);
            _cache.Remove(CacheKeys.UsersAll);

        }

        public async Task<List<UserManagementListDTO>> GetUsersAsync()
        {
            if (_cache.TryGetValue(CacheKeys.UsersAll, out List<UserManagementListDTO>? cached))
                return cached!;

            var users = await _repository.GetAllAsync();

            var result = users.Select(u => new UserManagementListDTO
            {
                Id = u.Id,
                Email = u.Email!,
                FullName = $"{u.FirstName} {u.LastName}"
            }).ToList();

            _cache.Set(CacheKeys.UsersAll, result);

            return result;
        }

        public async Task UpdateUserAsync(string id, UserManagementUpdateDTO dto)
        {
            var user = await _repository.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("user does not exist.");

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.MiddleName = dto.MiddleName;
           
            await _repository.UpdateUser(user);
            _cache.Remove(CacheKeys.UsersAll);

        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _repository.FindByIdAsync(id);
            if (user == null) throw new NotFoundException("User does not exist.");

            await _repository.DeleteUser(user);
            _cache.Remove(CacheKeys.UsersAll);
        }

     
    }
}

