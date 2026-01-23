using Backend.DTOs.Auth;
using Backend.DTOs.UserManagement;
using Backend.Exceptions;
using Backend.Models;
using Backend.Repository.UserManagement;

namespace Backend.Services.UserManagement
{
    public class UserManagementServices : IUserManagementServices
    {
        private readonly IUserManagementRepository _repository;
        
        public UserManagementServices(IUserManagementRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateUserAsync(UserManagementCreateDTO dto)
        {
            if (dto.Password != dto.confirmPassword)
                throw new BadRequestException("Password mismatched");

            var userInfo = new Users
            {
                UserName = user.username,
                Email = user.email,
                FirstName = user.firstName,
                MiddleName = user.middleName,
                LastName = user.lastName
            };

            await _repository.CreateUserAsync(userInfo, user.password);
        }

        public Task CreateUserAsync(UserManagementCreateDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserManagementReadDTO> GetUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserManagementListDTO>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(string id, UserManagementUpdateDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}

