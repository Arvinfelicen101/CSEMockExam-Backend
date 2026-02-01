namespace Backend.DTOs.Exams;

public record ParagraphDTO(int ParagraphId, string ParagraphText, List<QuestionsDTO> QuestionsText);