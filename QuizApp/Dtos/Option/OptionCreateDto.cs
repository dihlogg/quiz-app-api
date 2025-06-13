namespace QuizApp.Dtos.Option
{
    public class OptionCreateDto
    {
        public string? Text { get; set; }
        public bool IsCorrect { get; set; }
        public Guid? QuestionId { get; set; }
    }
}
