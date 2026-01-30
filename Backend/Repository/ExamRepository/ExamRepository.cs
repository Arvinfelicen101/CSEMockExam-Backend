using Backend.Context;
using Backend.Models;
using Backend.DTOs.Exams;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.ExamRepository;

public class ExamRepository : IExamRepository
{
    private readonly MyDbContext _context;

    public ExamRepository(MyDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<CategoryDTO>> GetAllAsync()
    {
        return await _context.Category
            .AsNoTracking()
            .AsSplitQuery()
            .Select(c => new CategoryDTO(
                c.Id,
                c.CategoryName.ToString(),
                c.SubCategoriesCollection.Select(sc => new SubCategoryDTO(
                    sc.Id,
                    sc.SubCategoryName,
                    sc.QuestionsCollection.Select(q => new QuestionsDTO(
                        q.Id,
                        q.QuestionName,
                        q.ChoicesCollection.Select(ch => new ChoicesDTO(
                            ch.Id,
                            ch.ChoiceText
                            )).ToList()
                        ))
                        .ToList()
                ))
                    .ToList()
            ))
            .ToListAsync<CategoryDTO>();
    }

    public async Task SubmitExamAsync(List<UserAnswers> answer)
    {
        await _context.UserAnswer.AddRangeAsync(answer);
    }
}