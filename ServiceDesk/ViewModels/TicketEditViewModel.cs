using ServiceDesk.Models.Enums;

namespace ServiceDesk.ViewModels
{
    public class TicketEditViewModel : TicketFormViewModel
    {
        public TicketStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
