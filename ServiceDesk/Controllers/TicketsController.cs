using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Services;
using ServiceDesk.Models.Enums;
using ServiceDesk.Models;
using ServiceDesk.ViewModels;

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
            var tickets = await _ticketsService.GetAllTickets(status, priority);
            return View(tickets);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = await _ticketsService.GetTicketFormViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TicketFormViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
    }
}
