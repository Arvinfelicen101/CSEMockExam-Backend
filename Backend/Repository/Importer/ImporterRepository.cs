using Backend.Context;
using Backend.Models;

namespace Backend.Repository.Importer;

public class ImporterRepository : IImporterRepository
{
    private readonly MyDbContext _context;

    public ImporterRepository(MyDbContext context)
    {
        _context = context;
    }

    public async Task SaveFKData(List<Category> categories, List<Paragraphs> paragraph, List<Choices> choices)
    {
        await _context.Category.AddRangeAsync(categories);
        await _context.Paragraph.AddRangeAsync(paragraph);
        await _context.Choice.AddRangeAsync(choices);
        await _context.SaveChangesAsync();
    }
}