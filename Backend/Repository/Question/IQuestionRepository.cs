using Backend.Models;

namespace Backend.Repository.Question
{
    public interface IQuestionRepository
    {
        Task AddQuestion(Questions question)
    }
}
