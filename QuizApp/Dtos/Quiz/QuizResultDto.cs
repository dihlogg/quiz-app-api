
namespace QuizApp.Dtos.Quiz
{
    public class QuizResultDto
    {
        public Guid QuizId { get; set; }
        public string UserName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double TotalTimeSeconds { get; set; }
        public int TotalCorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public bool Passed { get; set; }
        public List<QuizAnswerResultDto> Answers { get; set; }
    }
    public class QuizAnswerResultDto
    {
        public string QuestionText { get; set; }
        public string SelectedOptionText { get; set; }
        public bool IsCorrect { get; set; }
    }
}
