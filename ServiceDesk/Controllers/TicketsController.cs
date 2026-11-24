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
            var result = await _ticketsService.CreateTicketAsync(viewModel);
            if (result == UpdateTicketResult.InvalidData)
            {
                ModelState.AddModelError(string.Empty, "Bitte wählen Sie eine gültige Kategorie, Priorität und Zuständigkeit aus.");
                await _ticketsService.FillOptionsAsync(viewModel);
                return View(viewModel);
            }
            return result == UpdateTicketResult.Success
                ? RedirectToAction("Index")
                : StatusCode(500, "Ein unerwarteter Fehler ist aufgetreten.");
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
            if (ModelState.IsValid)
            {
                var result = await _ticketsService.UpdateTicketAsync(id, viewModel);
                switch (result)
                {
                    case UpdateTicketResult.Success:
                        return RedirectToAction("Index");
                    case UpdateTicketResult.NotFound:
                        return NotFound();
                    case UpdateTicketResult.InvalideStatus:
                        ModelState.AddModelError(string.Empty, "Abgeschlossene Tickets können nicht bearbeitet werden.");
                        break;
                    case UpdateTicketResult.InvalidData:
                        ModelState.AddModelError(string.Empty, "Bitte überprüfen Sie die eingegebenen Daten.");
                        break;
                    default:
                        return StatusCode(500, "Ein unerwarteter Fehler ist aufgetreten.");
                }
            }

            if (!await _ticketsService.PrepareEditViewModelAsync(id, viewModel))
            {
                return NotFound();
            }

            return View(viewModel);
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
                _ => StatusCode(500, "Ein unerwarteter Fehler ist aufgetreten."),
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ticketsService.DeleteTicketAsync(id);
            return result switch
            {
                UpdateTicketResult.Success => RedirectToAction("Index"),
                UpdateTicketResult.NotFound => NotFound(),
                _ => StatusCode(500, "Ein unerwarteter Fehler ist aufgetreten."),
            };
        }
    }
}
