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

    public async Task<TicketListViewModel> GetAllTicketsAsync(TicketStatus? status, TicketPriority? priority)
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
            .ToListAsync();
        var ticketListViewModel = new TicketListViewModel
        {
            Tickets = tickets,
            Statuses = Enum.GetNames(typeof(TicketStatus)).ToList(),
            Priorities = Enum.GetNames(typeof(TicketPriority)).ToList()
        };

        return ticketListViewModel;
    }

    public async Task<TicketFormViewModel> GetTicketFormViewModelAsync()
    {
        var viewModel = new TicketFormViewModel();
        await FillOptionsAsync(viewModel);
        return viewModel;
    }

    public async Task CreateTicketAsync(TicketFormViewModel viewModel)
    {
        _context.Tickets.Add(new Models.Ticket
        {
            Title = viewModel.Title,
            Description = viewModel.Description,
            Status = TicketStatus.Offen,
            Priority = viewModel.Priority!.Value,
            CreatedAt = DateTime.Now,
            CategoryId = viewModel.CategoryId!.Value,
            EmployeeId = viewModel.EmployeeId
        });
        await _context.SaveChangesAsync();
    }

    public async Task<TicketFormViewModel?> GetTicketFormViewModelAsync(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket == null)
        {
            return null;
        }
        var viewModel = new TicketEditViewModel
        {
            Title = ticket.Title,
            Description = ticket.Description,
            Priority = ticket.Priority,
            CategoryId = ticket.CategoryId,
            EmployeeId = ticket.EmployeeId,
            Status = ticket.Status,
            CreatedAt = ticket.CreatedAt
        };
        await FillOptionsAsync(viewModel);
        return viewModel;
    }

    public async Task<UpdateTicketResult> UpdateTicketAsync(int id, TicketEditViewModel viewModel)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null)
        {
            return UpdateTicketResult.NotFound;
        }
        if (ticket.Status == TicketStatus.Abgeschlossen)
        {
            return UpdateTicketResult.InvalideStatus;
        }

        ticket.Title = viewModel.Title;
        ticket.Description = viewModel.Description;
        ticket.Priority = viewModel.Priority!.Value;
        ticket.CategoryId = viewModel.CategoryId!.Value;
        ticket.Status = viewModel.Status;
        if (viewModel.EmployeeId.HasValue && !ticket.EmployeeId.HasValue)
        {
            ticket.EditDate = DateTime.Now;
            ticket.EmployeeId = viewModel.EmployeeId;
        }
        await _context.SaveChangesAsync();
        return UpdateTicketResult.Success;
    }

    internal async Task DeleteTicketAsync(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket != null)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }
    }

    internal async Task<UpdateTicketResult> ReopenTicketAsync(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket == null)
        {
            return UpdateTicketResult.NotFound;
        }
        if (ticket.Status != TicketStatus.Abgeschlossen)
        {
            return UpdateTicketResult.InvalideStatus;
        }
        ticket.Status = TicketStatus.Offen;
        ticket.CreatedAt = DateTime.Now;
        await _context.SaveChangesAsync();
        return UpdateTicketResult.Success;
    }

    public async Task FillOptionsAsync(TicketFormViewModel viewModel)
    {
        viewModel.Priorities = Enum.GetValues<TicketPriority>().ToList();
        viewModel.Categories = await _context.Categories.ToListAsync();
        viewModel.Employees = await _context.Employees.ToListAsync();
    }

}
