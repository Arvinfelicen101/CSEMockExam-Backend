using Backend.DTOs.Importer;
using Backend.Models;
namespace Backend.Services.Importer;

public static class ServiceHelper
{

    public static async Task<MappedData> ImportMapper(List<RawDataDTO> list)
    {
        var categories = new List<Category>();
        var paragraphs = new List<Paragraphs>();
        var choices = new List<Choices>();
        //fk first to be mapped, thrn implement caching
        foreach (var rowData in list)
        {
            categories.Add(new Category()
            {
                CategoryName = rowData.RawCategories
            });
           
            paragraphs.Add(new Paragraphs()
            {
                ParagraphText = rowData.RawParagraph
            });
            
            var allChoices = rowData.RawChoices.ToList();
            if (!string.IsNullOrWhiteSpace(rowData.RawAnswers))
                allChoices.Add(rowData.RawAnswers);

            choices.AddRange(allChoices.Select(c => new Choices { ChoiceText = c }));

        }

        return new MappedData(categories, paragraphs, choices);
    }
}