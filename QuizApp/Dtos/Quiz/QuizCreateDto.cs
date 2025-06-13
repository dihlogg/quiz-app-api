namespace QuizApp.Dtos.Quiz
{
    public class QuizCreateDto
    {
        public DateTime StartTime { get; set; }
        public string? Status { get; set; }
        public List<QuizAnswerDto> Answers { get; set; }
    }
}
