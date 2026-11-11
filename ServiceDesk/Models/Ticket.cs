using ServiceDesk.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceDesk.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        // Other properties...  
        [Required(ErrorMessage = "Der Titel ist erforderlich.")]
        [StringLength(100, ErrorMessage = "Der Titel darf maximal 100 Zeichen lang sind.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Die Beschreibung ist erforderlich.")]
        public string Description { get; set; }
        [Required]
        public TicketStatus Status { get; set; } = TicketStatus.Offen;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? EditDate { get; set; }
        public DateTime? CompleteDate { get; set; }

        // --- Foreign Keys (Fremdschlüssel) ---
        [Required(ErrorMessage = "Eine Kategorie muss ausgewählt werden.")]
        [Display(Name = "Kategorie")]
        public int CategoryId { get; set; }

        [Display(Name = "Zuständiger Mitarbeiter")]
        public int? Employee_ID { get; set; }

        // --- Navigation Properties (Beziehungen 1:n) ---
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("Employer_ID")]
        public virtual Employer Employer { get; set; }
    }
}
