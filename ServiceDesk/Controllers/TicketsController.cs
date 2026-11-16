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
            if (!ModelState.IsValid)
            {
                _ticketsService.FillOptions(viewModel);
                return View(viewModel);
            }
            await _ticketsService.CreateTicket(viewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _ticketsService.GetTicketFormViewModel();
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TicketFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                _ticketsService.FillOptions(viewModel);
                return View(viewModel);
            }

            var result = await _ticketsService.UpdateTicket(id, viewModel);
            switch (result)
            {
                case
                    UpdateTicketResult.Success:
                    return RedirectToAction("Index");
                case
                    UpdateTicketResult.NotFound:
                    return NotFound();
                case UpdateTicketResult.TicketClosed:
                    ModelState.AddModelError(string.Empty, "Cannot edit a closed ticket.");
                    _ticketsService.FillOptions(viewModel);
                    return View(viewModel);
                default:
                    return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
