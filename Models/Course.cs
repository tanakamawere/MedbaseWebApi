namespace MedbaseApi.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseRef { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public List<Topic> Topics { get; set; }
    }
}
