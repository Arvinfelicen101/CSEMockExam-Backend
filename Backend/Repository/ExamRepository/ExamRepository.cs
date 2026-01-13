using Backend.Context;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repository.ExamRepository;

public class ExamRepository : IExamRepository
{
    private readonly MyDbContext _context;

    public ExamRepository(MyDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Questions>> GetAllAsync()
    {
        return await _context.Question
            .Include(p => p.ParagraphNavigation)
            .Include(s => s.SubCategoryNavigation!)
            .ThenInclude(s => s.categoryNavigation)
            .Include(y => y.YearPeriodNavigation)
            .Include(c => c.ChoicesCollection)
            .ToListAsync();
    }

    public async Task SubmitExamAsync(List<UserAnswers> answer)
    {
        await _context.UserAnswer.AddRangeAsync(answer);
    }
}