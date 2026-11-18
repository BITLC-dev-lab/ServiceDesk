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
            var tickets = await _ticketsService.GetAllTicketsAsync(status, priority);
            return View(tickets);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = await _ticketsService.GetTicketFormViewModelAsync();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await _ticketsService.FillOptionsAsync(viewModel);
                return View(viewModel);
            }
            await _ticketsService.CreateTicketAsync(viewModel);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _ticketsService.GetTicketFormViewModelAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TicketEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await _ticketsService.FillOptionsAsync(viewModel);
                return View(viewModel);
            }

            var result = await _ticketsService.UpdateTicketAsync(id, viewModel);
            switch (result)
            {
                case
                    UpdateTicketResult.Success:
                    return RedirectToAction("Index");
                case
                    UpdateTicketResult.NotFound:
                    return NotFound();
                case UpdateTicketResult.InvalideStatus:
                    ModelState.AddModelError(string.Empty, "Cannot edit a closed ticket.");
                    await _ticketsService.FillOptionsAsync(viewModel);
                    return View(viewModel);
                default:
                    return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reopen(int Id)
        {
            var result = await _ticketsService.ReopenTicketAsync(Id);
            return result switch
            {
                UpdateTicketResult.Success => RedirectToAction("Edit", new { id = Id }),
                UpdateTicketResult.NotFound => NotFound(),
                UpdateTicketResult.InvalideStatus => BadRequest("Cannot reopen a ticket that is not closed."),
                _ => StatusCode(500, "An unexpected error occurred."),
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _ticketsService.DeleteTicketAsync(id);
            return RedirectToAction("Index");
        }
    }
}
