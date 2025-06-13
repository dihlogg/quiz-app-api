namespace QuizApp.Entities
{
    public class UserInfo : BaseEntities
    {
        public string? UserName { get; set; }
        public string? TotalTime { get; set; }
        public int? QuizAttempts { get; set; }
        public ICollection<QuizInfo> QuizInfos { get; set; }
    }
}
