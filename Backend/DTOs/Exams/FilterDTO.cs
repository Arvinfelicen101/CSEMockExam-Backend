namespace Backend.DTOs.Exams;

public class FilterDTO
{
    public List<string>? CategoryFilter { get; init; }
    public List<string>? SubCategoryFilter { get; init; }
}
