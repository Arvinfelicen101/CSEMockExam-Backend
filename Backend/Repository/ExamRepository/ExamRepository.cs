using Backend.Context;
using Backend.Models;
namespace Backend.Repository.ExamRepository;

public class ExamRepository : IExamRepository
{
    private readonly MyDbContext _context;

    public ExamRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task SubmitExamAsync(List<UserAnswers> answer)
    {
        await _context.UserAnswer.AddRangeAsync(answer);
    }
}