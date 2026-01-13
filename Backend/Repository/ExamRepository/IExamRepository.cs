using Backend.Models;

namespace Backend.Repository.ExamRepository;

public interface IExamRepository
{
    Task SubmitExamAsync(List<UserAnswers> answer);
    Task<List<Questions>> GetAllAsync();
}