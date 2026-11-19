using System.ComponentModel.DataAnnotations;
using ServiceDesk.Models.Enums;

namespace ServiceDesk.ViewModels
{
    public class TicketEditViewModel : TicketFormViewModel
    {
        [Required(ErrorMessage = "Der Status ist erforderlich.")]
        public TicketStatus? Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
