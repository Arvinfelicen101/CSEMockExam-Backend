using Backend.DTOs.Question;
namespace Backend.DTOs.Exams;

public class SubCategoryDTO
{
    public required List<QuestionListDTO> QuestionListDtos { get; set; }
}