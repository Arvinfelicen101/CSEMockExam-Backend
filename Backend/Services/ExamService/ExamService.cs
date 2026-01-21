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

    public List<ExamDTO> MapQuestions(List<Questions> questions)
    {
        var mappedData = new List<ExamDTO>();
        foreach (var q in questions)
        {
            mappedData.Add(new ExamDTO()
            {
                // QuestionName = q.QuestionName,
                // // SubCategoryId = q.SubCategoryId,
                // // SubCategoryName = q.SubCategoryNavigation!.SubCategoryName,
                // ParagraphId = q.ParagraphId,
                // ParagraphTxt = q.ParagraphNavigation!.ParagraphText,
                // YearPeriodId = q.YearPeriodId,
                // year = q.YearPeriodNavigation!.Year,
                // period = q.YearPeriodNavigation.Periods.ToString()
                
                category = new Category()
                {
                    Id = q.SubCategoryNavigation!.CategoryId,
                    CategoryName = q.SubCategoryNavigation!.categoryNavigation!.CategoryName
                },
                SubCategoryDto = new List<SubCategoryDTO>()
                {
                    new SubCategoryDTO()
                    {
                        
                    }
                }
                
            });
        }
        return mappedData;
    }
    
    //create check exam answer results here
    // public async Task
}