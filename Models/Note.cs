namespace MedbaseApi.Models;

public class Note
{
    public int Id { get; set; }
    public int TopicReference { get; set; }
    public DateTime DateUpdated { get; set; } = DateTime.Now;
    public string Text { get; set; }
}
