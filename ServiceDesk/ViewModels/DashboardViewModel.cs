using ServiceDesk.Models;

namespace ServiceDesk.ViewModels
{
    public class DashboardViewModel 
    {
        
        public int TotalTickets { get; set; }
        public int OpenTickets { get; set; }
        public int InProgressTickets { get; set; }
        public int CompletedTickets { get; set; }
        public int HighPriorityTickets { get; set; }
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

    }
}

