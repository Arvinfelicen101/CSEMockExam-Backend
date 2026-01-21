using Backend.DTOs.Exams;
using Backend.DTOs.Question;

namespace Backend.Services.ExamService;

public interface IExamService
{
    Task SubmitExamService(List<UserExamAnswerDTO> exam);
    Task<List<QuestionListDTO>> GetAllAsync();
}