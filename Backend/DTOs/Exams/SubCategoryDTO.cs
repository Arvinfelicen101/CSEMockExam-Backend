namespace Backend.DTOs.Exams;

public record SubCategoryDTO(
    int SubCategoryId, 
    string SubCategoryName, 
    // List<ParagraphDTO> Paragraphs, 
    List<QuestionsDTO> Questions
);