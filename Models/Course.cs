using System.ComponentModel.DataAnnotations;

namespace MedbaseApi.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a course reference")]
        public string CourseRef { get; set; }
        [Required(ErrorMessage = "Please enter a course title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter a course description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter an image url")]
        public string ImageURL { get; set; }
    }

    public class CourseArticlesDto
    {
        public List<Course> Courses { get; set; }
        public List<Article> Articles { get; set; }
    }
}
