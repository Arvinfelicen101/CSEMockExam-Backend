using Backend.Models;

namespace Backend.DTOs.Importer;

public class MappedDataFK
{
    public List<SubCategories>  SubCategoriesList { get; set; }
    public List<YearPeriods> YearPeriodsList { get; set; }
    public List<Paragraphs> ParagraphsList {get; set; }
}