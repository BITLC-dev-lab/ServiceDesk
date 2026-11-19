using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Data;
using ServiceDesk.Models.Enums;
using ServiceDesk.ViewModels;


namespace ServiceDesk.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = new DashboardViewModel
            {
                TotalTickets = await _context.Tickets.CountAsync(),
                OpenTickets = await _context.Tickets.CountAsync(t => t.Status == Models.Enums.TicketStatus.Offen),
                InProgressTickets = await _context.Tickets.CountAsync(t => t.Status == Models.Enums.TicketStatus.InBearbeitung),
                CompletedTickets = await _context.Tickets.CountAsync(t => t.Status == Models.Enums.TicketStatus.Abgeschlossen),
                HighPriorityTickets = await _context.Tickets.CountAsync(t => t.Priority == Models.Enums.TicketPriority.Hoch),            
                Tickets = await _context.Tickets
                .Include(t => t.Category)
                .Include(t => t.Employee)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync()
            };
            return View(viewModel);
        }
    }
}
