namespace ServiceDesk.ViewModels
{
    public class TicketListViewModel
    {
        public List<TicketViewModel> Tickets { get; set; } = new List<TicketViewModel>();
        public List<string> Statuses { get; set; } = new List<string>();
        public List<string> Priorities { get; set; } = new List<string>();
    }
}
