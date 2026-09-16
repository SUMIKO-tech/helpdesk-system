public class Ticket
{
    public int TicketId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Priority { get; set; } = string.Empty; // Low, Medium, High
    public string Status { get; set; } = "Open"; // Open, InProgress, Resolved, Closed
    public int CreatedBy { get; set; }
    public User? CreatedByUser { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public ICollection<TicketComment> Comments { get; set; } = new List<TicketComment>();
}