namespace Backend.DTOs.Exams;

public record CategoryDTO(int Id, string CategoryName, List<SubCategoryDTO> SubCategory);