namespace MedbaseApi.Models
{
    public class QuestionPaged
    {
        public IEnumerable<Question> Questions { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
