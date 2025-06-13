using QuizApp.Dtos.Option;

namespace QuizApp.Dtos.Quiz
{
    public class QuizDto : QuizCreateDto
    {
        public Guid Id { get; set; }
    }
    public class QuizAnswerDto
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<OptionDto> Options { get; set; }
    }
}
