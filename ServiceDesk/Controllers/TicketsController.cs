using ServiceDesk.Data;
using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Services;

namespace ServiceDesk.Controllers
{
    public class TicketsController : Controller
    {
        private TicketsService _ticketsService;
        private ApplicationDbContext _context;
        public TicketsController(TicketsService ticketsService, ApplicationDbContext context)
        {
            _ticketsService = ticketsService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketsService.GetAllTicketsAsync();
            return View(tickets);
        }
    }
}
