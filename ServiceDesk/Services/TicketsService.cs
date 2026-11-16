using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ServiceDesk.Data;
using ServiceDesk.ViewModels;
using ServiceDesk.Models.Enums;
using Microsoft.AspNetCore.Http.HttpResults;
namespace ServiceDesk.Services;


public class TicketsService
{
    private readonly ApplicationDbContext _context;

    public TicketsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TicketListViewModel>> GetAllTickets(TicketStatus? status, TicketPriority? priority)
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

    public async Task<TicketFormViewModel> GetTicketFormViewModel()
    {
        var viewModel = new TicketFormViewModel();
        FillOptions(viewModel);
        return viewModel;
    }

    public async Task CreateTicket(TicketFormViewModel viewModel)
    {
        _context.Tickets.Add(new Models.Ticket
        {
            Title = viewModel.Title,
            Description = viewModel.Description,
            Status = TicketStatus.Offen,
            Priority = viewModel.Priority!.Value,
            CreatedAt = DateTime.Now,
            CategoryId = viewModel.CategoryId!.Value,
        });
        await _context.SaveChangesAsync();
    }

    public async Task<UpdateTicketResult> UpdateTicket(int id, TicketFormViewModel viewModel)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null)
        {
            return UpdateTicketResult.NotFound;
        }
        if (ticket.Status == TicketStatus.Abgeschlossen)
        {
            return UpdateTicketResult.TicketClosed;
        }

        ticket.Title = viewModel.Title;
        ticket.Description = viewModel.Description;
        ticket.Priority = viewModel.Priority!.Value;
        ticket.CategoryId = viewModel.CategoryId!.Value;
        if (viewModel.EmployeeId.HasValue && !ticket.EmployeeId.HasValue)
        {
            ticket.EditDate = DateTime.Now;
            ticket.EmployeeId = viewModel.EmployeeId;
        }
        await _context.SaveChangesAsync();
        return UpdateTicketResult.Success;
    }

    public void FillOptions(TicketFormViewModel viewModel)
    {
        viewModel.Priorities = Enum.GetValues<TicketPriority>().ToList();
        viewModel.Categories = _context.Categories.ToList();
        viewModel.Employees = _context.Employees.ToList();
    }
}
