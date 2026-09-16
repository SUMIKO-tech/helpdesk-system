public class CreateTicketRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Priority { get; set; } = "Medium"; // Low, Medium, High
}

public class UpdateTicketStatusRequest
{
    public string Status { get; set; } = string.Empty; // Open, InProgress, Resolved, Closed
}

public class UpdateTicketPriorityRequest
{
    public string Priority { get; set; } = string.Empty; // Low, Medium, High
}

public class TicketResponse
{
    public int TicketId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Priority { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string CreatedByName { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}