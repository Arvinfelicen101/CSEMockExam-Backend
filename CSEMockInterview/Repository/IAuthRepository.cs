using CSEMockInterview.Models;

namespace CSEMockInterview.Repository
{
    public interface IAuthRepository
    {
        Task<Users?> CheckUserRepository(Users user);
    }
}
