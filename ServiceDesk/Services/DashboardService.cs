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
            var filteredQuery = query;

            if (status.HasValue)
                filteredQuery = filteredQuery.Where(t => t.Status == status.Value);

            if (priority.HasValue)
                filteredQuery = filteredQuery.Where(t => t.Priority == priority.Value);

            var tickets = new List<TicketViewModel>();

                foreach (var ticket in query)
                {
                    tickets.Add(new TicketViewModel
                    {
                        Id = ticket.Id,
                        Title = ticket.Title,
                        CategoryName = ticket.Category.Name,
                        Priority = ticket.Priority,
                        Status = ticket.Status,
                        EmployeeName = ticket.Employee != null ? ticket.Employee.FirstName + " " + ticket.Employee.LastName : string.Empty,
                        CreatedAt = ticket.CreatedAt
                    });
                }
                var viewModel = new DashboardViewModel
                {
                    TotalTickets = query.Count(),
                    OpenTickets = query.Count(t => t.Status == TicketStatus.Offen),
                    InProgressTickets = query.Count(t => t.Status == TicketStatus.InBearbeitung),
                    CompletedTickets = query.Count(t => t.Status == TicketStatus.Abgeschlossen),
                    HighPriorityTickets = query.Count(t => t.Priority == TicketPriority.Hoch),
                    Tickets = tickets
                };
            return viewModel;
        }
    }
}

