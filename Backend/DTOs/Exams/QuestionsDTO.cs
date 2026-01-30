namespace Backend.DTOs.Exams;

public record QuestionsDTO(int QuestionId, string QuestionText, List<ChoicesDTO> Choices);