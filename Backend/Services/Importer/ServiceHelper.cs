using System.Text;
using Backend.DTOs.Importer;
using Backend.Models.enums;
using Backend.Models;
using ClosedXML.Excel;
using Paragraphs = Backend.Models.Paragraphs;

namespace Backend.Services.Importer;

public static class ServiceHelper
{
    public static async Task<List<RawDataDTO>> ParseFileAsync(ImporterDTO xlsx)
    {
        string sheetName;
        var extractedData = new List<RawDataDTO>();
        using (var stream = new MemoryStream())
        {
            var filename = xlsx.file.FileName;
            string year = filename.Substring(0, 3);
            int i = 5;
            var period = new StringBuilder();

            while (i < filename.Length)
            {
                char charPeriod = filename[i];

                if (charPeriod == '_')
                    break;

                period.Append(charPeriod);
                i++;
            }

            string result = period.ToString();
            await xlsx.file.CopyToAsync(stream);
            try
            {
                using (var workbook = new XLWorkbook(stream))
                {
                    foreach (var worksheet in workbook.Worksheets)
                    {
                        sheetName = worksheet.Name;
                        
                        var rows = worksheet.RowsUsed().Skip(1);
                        foreach (var row in rows)
                        {
                            extractedData.Add(new RawDataDTO()
                            {
                                RawCategories = sheetName,
                                RawQuestions = row.Cell(1).GetString(),
                                RawSubCategories = row.Cell(2).GetString(),
                                RawChoices = new List<ChoiceDTO>()
                                {
                                    
                                    new ChoiceDTO()
                                    {
                                        ChoiceText = row.Cell(3).GetString(),
                                        IsCorrect = false
                                    },
                                    new ChoiceDTO()
                                    {
                                        ChoiceText = row.Cell(4).GetString(),
                                        IsCorrect = false
                                    },
                                    new ChoiceDTO()
                                    {
                                        ChoiceText = row.Cell(5).GetString(),
                                        IsCorrect = false
                                    },
                                    new ChoiceDTO()
                                    {
                                        ChoiceText = row.Cell(6).GetString(),
                                        IsCorrect = true
                                    }
                                    
                                },
                            
                                RawParagraph = row.Cell(7).GetString(),
                                RawYear = Convert.ToInt32(year),
                                RawPeriods = result
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception();
            }
            
        }
        return extractedData;
    }
    

    //create a dto for the list of fks, should be in service if it need to insert data, possible to be not. just separate method
    public static async Task<List<Questions>> ImportFKMapper(List<RawDataDTO> list, FKDataDTOs dtos)
    {
        //PREPARE CACHE VARIABLES FIRST / dictionaries
        var paragraphCache = dtos.ParagraphFK.ToDictionary(p => p.ParagraphText, p => p);
        var yearPeriodCache = dtos.YearPeriodFK.ToDictionary(y => (y.Year, y.Periods.ToString()), y => y);
        var subCategoryCache = new Dictionary<string, SubCategories>();
        var categoryMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            ["Verbal"] = 1,
            ["Analytical"] = 2,
            ["Numerical"] = 3,
            ["Clerical"] = 4,
            ["General"] = 5
        };
        //need linq 
        var questions = new List<Questions>();
        var choices = new List<Choices>();
        
        
        foreach (var rowData in list)
        {
            //just check every fk if it exist or not in the cache

            if (!paragraphCache.TryGetValue(rowData.RawParagraph, out var paragraph))
            {
                paragraph = new Paragraphs()
                {
                    ParagraphText = rowData.RawParagraph
                };
            }

            if (!yearPeriodCache.TryGetValue((rowData.RawYear, rowData.RawPeriods), out var yearPeriods))
            {
                if (Enum.TryParse<Periods>(rowData.RawPeriods, true, out var period))
                {
                    rowData.RawPeriods = period.ToString();
                }

                yearPeriods = new YearPeriods()
                {
                    Year = rowData.RawYear,
                    Periods = period
                };
            }
            
            if (!subCategoryCache.TryGetValue(rowData.RawSubCategories, out var subCategories))
            {
                if (!categoryMap.TryGetValue(rowData.RawCategories, out var categoryId))
                    throw new Exception($"Unknown category: {rowData.RawCategories}");

                subCategories = new SubCategories
                {
                    SubCategoryName = rowData.RawSubCategories,
                    CategoryId = categoryId
                };
                
                subCategoryCache[rowData.RawSubCategories] = subCategories;
            }
            
            //might need mapping and lookup for category
            questions.Add(new Questions()
            {
                QuestionName = rowData.RawQuestions,
                ParagraphNavigation = paragraph,
                SubCategoryNavigation = subCategories,
                YearPeriodNavigation = yearPeriods,
            });
            
            choices.Add(new Choices()
            {

            });
        }

        return questions;
    }
    
}