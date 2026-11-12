using ServiceDesk.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceDesk.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Der Titel ist erforderlich.")]
        [StringLength(100, ErrorMessage = "Der Titel darf maximal 100 Zeichen lang sind.")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Die Beschreibung ist erforderlich.")]
        public string Description { get; set; } = string.Empty;
        public TicketStatus Status { get; set; } = TicketStatus.Offen;
        [Required(ErrorMessage = "Die Priorität ist erforderlich.")]
        public TicketPriority Priority { get; set; } = TicketPriority.Niedrig;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? EditDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        [Required(ErrorMessage = "Eine Kategorie muss ausgewählt werden.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

    }
}
