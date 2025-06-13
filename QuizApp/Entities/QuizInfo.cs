namespace QuizApp.Entities
{
    public class QuizInfo : BaseEntities
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Status { get; set; }
        public string? QuizStatus { get; set; }
        public Guid? UserInfoId { get; set; }
        public UserInfo? UserInfos { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
