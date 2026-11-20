using ServiceDesk.Models;
using ServiceDesk.ViewModels;

namespace ServiceDesk.ViewModels
{
    public class DashboardViewModel 
    {
        
        public int TotalTickets { get; set; }
        public int OpenTickets { get; set; }
        public int InProgressTickets { get; set; }
        public int CompletedTickets { get; set; }
        public int HighPriorityTickets { get; set; }
        public List<TicketViewModel> Tickets { get; set; } = new();

    }
}

