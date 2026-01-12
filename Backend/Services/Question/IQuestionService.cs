using Backend.DTOs.Question;
using Backend.Models;

namespace Backend.Services.Question
{
    public interface IQuestionService
    {
        Task CreateQuestionAsync(QuestionCreateDTO question);

        Task <QuestionReadDTO> GetQuestionByIdAsync(int id);
        Task<List<QuestionListDTO>> GetAllAsync();

        Task UpdateQuestionAsync(int id, QuestionUpdateDTO question);

        Task DeleteQuestionAsync(int id);
    }
}
