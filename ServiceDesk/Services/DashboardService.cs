using Microsoft.EntityFrameworkCore;
using ServiceDesk.Data;
using ServiceDesk.Models;
using ServiceDesk.Models.Enums;
using ServiceDesk.ViewModels;
using System.Net.NetworkInformation;

namespace ServiceDesk.Services
{
    public class DashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DashboardViewModel> GetDashboardViewModelAsync(TicketStatus? status, TicketPriority? priority)
        {
            var query = _context.Tickets.AsNoTracking();
            var allTickets = query;

            if (status.HasValue)
                query = query.Where(t => t.Status == status.Value);

            if (priority.HasValue)
                query = query.Where(t => t.Priority == priority.Value);

            var tickets = await query
                .Select(t => new TicketViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    CategoryName = t.Category.Name,
                    Priority = t.Priority,
                    Status = t.Status,
                    EmployeeName = t.Employee != null ? t.Employee.FirstName + " " + t.Employee.LastName : string.Empty,
                    CreatedAt = t.CreatedAt
                })
                .OrderByDescending(t => t.CreatedAt)
                .ThenByDescending(t => t.Id)
                .ToListAsync();
            
                var viewModel = new DashboardViewModel
                {
                    TotalTickets = allTickets.Count(),
                    OpenTickets = allTickets.Count(t => t.Status == TicketStatus.Offen),
                    InProgressTickets = allTickets.Count(t => t.Status == TicketStatus.InBearbeitung),
                    CompletedTickets = allTickets.Count(t => t.Status == TicketStatus.Abgeschlossen),
                    HighPriorityTickets = allTickets.Count(t => t.Priority == TicketPriority.Hoch),
                    Tickets = tickets
                };
            return viewModel;
        }
    }
}

