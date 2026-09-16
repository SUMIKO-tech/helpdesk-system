using System.Net.Sockets;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty; // "Employee" or "Admin"

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}