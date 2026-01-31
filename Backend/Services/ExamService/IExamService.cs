using Backend.DTOs.Exams;

namespace Backend.Services.ExamService;

public interface IExamService
{
    Task SubmitExamService(UserExamAnswerDTO exam);
    Task<List<CategoryDTO>> GetAllAsync();
    Task<List<CategoryDTO>> GetFilteredQuestions(FilterDTO data);
}