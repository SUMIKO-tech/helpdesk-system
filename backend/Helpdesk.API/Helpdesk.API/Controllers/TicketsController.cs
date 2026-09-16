using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TicketsController : ControllerBase
{
    private readonly AppDbContext _db;

    public TicketsController(AppDbContext db)
    {
        _db = db;
    }

    private int CurrentUserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    private bool IsAdmin => User.IsInRole("admin");

    // GET: api/tickets
    // Employee -> only their own tickets
    // Admin -> all tickets
    [HttpGet]
    public async Task<IActionResult> GetTickets()
    {
        var query = _db.Tickets.Include(t => t.CreatedByUser).AsQueryable();

        if (!IsAdmin)
            query = query.Where(t => t.CreatedBy == CurrentUserId);

        var tickets = await query
            .OrderByDescending(t => t.CreatedDate)
            .Select(t => new TicketResponse
            {
                TicketId = t.TicketId,
                Title = t.Title,
                Description = t.Description,
                Priority = t.Priority,
                Status = t.Status,
                CreatedByName = t.CreatedByUser!.Name,
                CreatedDate = t.CreatedDate
            })
            .ToListAsync();

        return Ok(tickets);
    }

    // GET: api/tickets/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicket(int id)
    {
        var ticket = await _db.Tickets.Include(t => t.CreatedByUser)
            .FirstOrDefaultAsync(t => t.TicketId == id);

        if (ticket == null) return NotFound();

        if (!IsAdmin && ticket.CreatedBy != CurrentUserId)
            return Forbid();

        return Ok(new TicketResponse
        {
            TicketId = ticket.TicketId,
            Title = ticket.Title,
            Description = ticket.Description,
            Priority = ticket.Priority,
            Status = ticket.Status,
            CreatedByName = ticket.CreatedByUser!.Name,
            CreatedDate = ticket.CreatedDate
        });
    }

    // POST: api/tickets  (Employee creates a ticket)
    [HttpPost]
    public async Task<IActionResult> CreateTicket(CreateTicketRequest request)
    {
        var ticket = new Ticket
        {
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            Status = "Open",
            CreatedBy = CurrentUserId,
            CreatedDate = DateTime.Now
        };

        _db.Tickets.Add(ticket);
        await _db.SaveChangesAsync();

        return Ok(new { message = "Ticket created.", ticketId = ticket.TicketId });
    }

    // PUT: api/tickets/5/status  (Admin only)
    [HttpPut("{id}/status")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateStatus(int id, UpdateTicketStatusRequest request)
    {
        var ticket = await _db.Tickets.FindAsync(id);
        if (ticket == null) return NotFound();

        ticket.Status = request.Status;
        await _db.SaveChangesAsync();

        return Ok(new { message = "Status updated." });
    }

    // PUT: api/tickets/5/priority  (Admin only)
    [HttpPut("{id}/priority")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdatePriority(int id, UpdateTicketPriorityRequest request)
    {
        var ticket = await _db.Tickets.FindAsync(id);
        if (ticket == null) return NotFound();

        ticket.Priority = request.Priority;
        await _db.SaveChangesAsync();

        return Ok(new { message = "Priority updated." });
    }
}