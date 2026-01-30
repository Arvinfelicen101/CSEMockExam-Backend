using Backend.Repository.ExamRepository;
using Backend.DTOs.Exams;
using Backend.Models;
namespace Backend.Services.ExamService;

public class ExamService : IExamService
{
    private readonly IExamRepository _repository;

    public ExamService(IExamRepository repository)
    {
        _repository = repository;
    }

    public async Task SubmitExamService(List<UserExamAnswerDTO> exam)
    {
        var mapped = MapResults(exam);
        await _repository.SubmitExamAsync(mapped);
    }

    private List<UserAnswers> MapResults(List<UserExamAnswerDTO> dtos)
    {
        var mappedData = new List<UserAnswers>();
        foreach (var answer in dtos)
        {
            mappedData.Add(new UserAnswers()
            {
                UserId = answer.UserId,
                QuestionId = answer.AnswerId,
                Answer = answer.AnswerId
            });
        }

        return mappedData;
    }
    
    public async Task<List<CategoryDTO>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<List<CategoryDTO>> GetFilteredQuestions(FilterDTO data)
    {
        return await _repository.FetchFilteredData(data);
    }
    
}