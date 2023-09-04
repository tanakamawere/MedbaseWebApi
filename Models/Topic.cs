using System.ComponentModel.DataAnnotations;

namespace MedbaseApi.Models
{
    public class Topic
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Select a course reference")]
        public string CourseRef { get; set; }
        [Required(ErrorMessage = "Enter a topic reference")]
        public int TopicRef { get; set; }
        [Required(ErrorMessage = "Enter a course name")]
        public string Name { get; set; }
    }
}
