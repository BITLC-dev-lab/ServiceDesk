using Microsoft.EntityFrameworkCore;
using ServiceDesk.Data;
using ServiceDesk.ViewModels;
using ServiceDesk.Models.Enums;
namespace ServiceDesk.Services;


public class TicketsService
{
    private readonly ApplicationDbContext _context;

    public TicketsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TicketListViewModel>> GetAllTicketsAsync(TicketStatus? status, TicketPriority? priority)
    {
        var query = _context.Tickets.AsNoTracking();

        if (status.HasValue)
        {
            query = query.Where(t => t.Status == status.Value);
        }

        if (priority.HasValue)
        {
            query = query.Where(t => t.Priority == priority.Value);
        }

        var tickets = await query
            .Select(t => new TicketListViewModel
            {
                Id = t.Id,
                Title = t.Title,
                CategoryName = t.Category.Name,
                Priority = t.Priority.ToString(),
                Status = t.Status.ToString(),
                EmployeeName = t.Employee != null ? t.Employee.FirstName + " " + t.Employee.LastName : string.Empty,
                CreatedAt = t.CreatedAt
            })
            .ToListAsync();
        return tickets;
    }
}
