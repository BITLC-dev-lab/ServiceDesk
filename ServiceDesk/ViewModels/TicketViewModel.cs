using ServiceDesk.Models.Enums;

namespace ServiceDesk.ViewModels
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public TicketPriority Priority { get; set; }
        public TicketStatus Status { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
