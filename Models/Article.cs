namespace MedbaseApi.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? Summary { get; set; }
        public string? Writer { get; set; }
        public DateTime DatePosted { get; set; }
        public string? ImageURL { get; set; }
    }
}
