namespace Backend.DTOs.Exams;

public record FilterDTO(
    List<string>? CategoryFilter,
    List<string>? SubCategoryFilter,
    int QuestionNumber
    );