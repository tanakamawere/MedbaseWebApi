namespace MedbaseApi.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? Reference { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public double Amount { get; set; }
        public SubTier? SubTier { get; set; }
    }
}
