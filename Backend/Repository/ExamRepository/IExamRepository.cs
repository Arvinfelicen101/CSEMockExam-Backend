using Backend.Models;
using Backend.DTOs.Exams;
namespace Backend.Repository.ExamRepository;

public interface IExamRepository
{
    Task SubmitExamAsync(List<UserAnswers> answer);
    Task<List<CategoryDTO>> GetAllAsync();
    Task<List<CategoryDTO>> FetchFilteredData(FilterDTO dto);
}