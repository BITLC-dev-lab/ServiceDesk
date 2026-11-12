namespace ServiceDesk.ViewModels
{
    public class TicketListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string? Status { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
