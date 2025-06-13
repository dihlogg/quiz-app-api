namespace QuizApp.Entities
{
    public class Option : BaseEntities
    {
        public string? Text { get; set; }
        public bool IsCorrect { get; set; }
        public Guid? QuestionId { get; set; }
        public Question? Questions { get; set; }
    }
}
