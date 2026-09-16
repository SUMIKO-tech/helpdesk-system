using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<TicketComment> TicketComments => Set<TicketComment>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.CreatedByUser)
            .WithMany(u => u.Tickets)
            .HasForeignKey(t => t.CreatedBy);

        modelBuilder.Entity<TicketComment>()
            .HasOne(tc => tc.Ticket)
            .WithMany(t => t.Comments)
            .HasForeignKey(tc => tc.TicketId);

        modelBuilder.Entity<TicketComment>()
            .HasOne(tc => tc.CreatedByUser)
            .WithMany()
            .HasForeignKey(tc => tc.CreatedBy);
    }
}