using Backend.DTOs.Exams;

namespace Backend.Services.ExamService;

public interface IExamService
{
    Task SubmitExamService(List<UserExamAnswerDTO> exam);
}