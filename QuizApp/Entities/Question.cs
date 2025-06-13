namespace QuizApp.Entities
{
    public class Question : BaseEntities
    {
        public string Text { get; set; }
        public ICollection<Option> Options { get; set; }
    }
}
