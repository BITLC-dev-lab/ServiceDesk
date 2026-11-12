using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Services;
using ServiceDesk.Models.Enums;

namespace ServiceDesk.Controllers
{
    public class TicketsController : Controller
    {
        private readonly TicketsService _ticketsService;
        public TicketsController(TicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }
        public async Task<IActionResult> Index(TicketStatus? status, TicketPriority? priority)
        {
            var tickets = await _ticketsService.GetAllTicketsAsync(status, priority);
            return View(tickets);
        }
    }
}
