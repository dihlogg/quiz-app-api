namespace QuizApp.Dtos.UserInfo
{
    public class UserInfoCreateDto
    {
        public string? UserName { get; set; }
        public string? TotalTime { get; set; }
        public int? QuizAttempts { get; set; }
    }
}
