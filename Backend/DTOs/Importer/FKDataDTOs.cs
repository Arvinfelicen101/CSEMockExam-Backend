using Backend.Models;

namespace Backend.DTOs.Importer;

public class FKDataDTOs
{
    public IEnumerable<YearPeriods> YearPeriodFK { get; set; }
    public IEnumerable<Paragraphs> ParagraphFK { get; set; }
    public IEnumerable<SubCategories> subCategoriesFK { get; set; }
}