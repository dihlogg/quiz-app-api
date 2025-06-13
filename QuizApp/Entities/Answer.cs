namespace QuizApp.Entities
{
    public class Answer : BaseEntities
    {
        public bool? IsCorrect { get; set; }
        //quiz
        public Guid? QuizInfoId { get; set; }
        public QuizInfo? QuizInfos { get; set; }
        //question
        public Guid? QuestionId { get; set; }
        public Question? Questions { get; set; }
        //option
        public Guid? SelectedOptionId { get; set; }
        public Option? SelectedOptions { get; set; }
    }
}
