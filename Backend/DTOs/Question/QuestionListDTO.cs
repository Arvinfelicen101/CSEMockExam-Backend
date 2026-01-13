namespace Backend.DTOs.Question
{
    public class QuestionListDTO
    {
        public required string QuestionName {  get; set; }
        public required int categoryId { get; set; }
        public required string categoryName { get; set; }
        public required int SubCategoryId { get; set; }
        public required string SubCategoryName { get; set; }
        public int? ParagraphId { get; set; }
        public string? ParagraphTxt { get; set; }
        public int YearPeriodId { get; set; }
        public required int year { get; set; }
        public required string period { get; set; }
  
    }
}
