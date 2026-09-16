using System.ComponentModel.DataAnnotations;

public class TicketComment
{
    [Key]
    public int CommentId { get; set; }
    public int TicketId { get; set; }
    public Ticket? Ticket { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int CreatedBy { get; set; }
    public User? CreatedByUser { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}