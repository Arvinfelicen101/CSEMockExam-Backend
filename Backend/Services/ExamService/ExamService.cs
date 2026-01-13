using Backend.Repository.ExamRepository;
using Backend.DTOs.Exams;
using Backend.DTOs.Question;
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
    
    public async Task<List<QuestionListDTO>> GetAllAsync()
    {
           
        var result = await _repository.GetAllAsync();
        var mapped = MapQuestions(result);
        return mapped;      
    }

    public List<QuestionListDTO> MapQuestions(List<Questions> questions)
    {
        var mappedData = new List<QuestionListDTO>();
        foreach (var q in questions)
        {
            mappedData.Add(new QuestionListDTO()
            {
                QuestionName = q.QuestionName,
                categoryId = q.SubCategoryNavigation!.categoryNavigation!.Id,
                categoryName = q.SubCategoryNavigation.categoryNavigation.CategoryName.ToString(),
                SubCategoryId = q.SubCategoryId,
                SubCategoryName = q.SubCategoryNavigation.SubCategoryName,
                ParagraphId = q.ParagraphId,
                ParagraphTxt = q.ParagraphNavigation!.ParagraphText,
                YearPeriodId = q.YearPeriodId,
                year = q.YearPeriodNavigation!.Year,
                period = q.YearPeriodNavigation.Periods.ToString()
            });
                
        }

        return mappedData;
    }
    
    //create check exam answer results here
    // public async Task
}