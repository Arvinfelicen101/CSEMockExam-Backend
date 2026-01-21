using Backend.DTOs.Question;
using Backend.Models;
namespace Backend.DTOs.Exams;

public class ExamDTO
{
    public required Category category { get; set; }
    public required List<SubCategoryDTO> SubCategoryDto { get; set; }
    
}