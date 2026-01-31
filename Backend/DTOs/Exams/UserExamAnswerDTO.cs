namespace Backend.DTOs.Exams;

public class UserExamAnswerDTO
{
    // CHANGE TO LIST OF ANSWERS FIELD AND A DURATION FIELD INSTEAD
    public required List<AnswersListDTO> UserAnswer { get; set; }
    public required TimeSpan duration { get; set; }
    //add duration of exam
}