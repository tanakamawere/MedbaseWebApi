namespace MedbaseApi.Models
{
    public class QuizSession
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime TimeStarted { get; set; } = DateTime.Now;
        public DateTime TimeEnded { get; set; } = DateTime.Now;
        public int NumberOfQuestions { get; set; }
        public int NumberCorrect { get; set; }
        public int NumberWrong { get; set; }
        public int NumberLeft { get; set; }
    }
}
