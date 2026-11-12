using System.ComponentModel.DataAnnotations;
using ServiceDesk.Models;
using ServiceDesk.Models.Enums;

namespace ServiceDesk.ViewModels

{
    public class TicketFormViewModel
    {
        [Required(ErrorMessage = "Der Titel ist erforderlich.")]
        [StringLength(100, ErrorMessage = "Der Titel darf maximal 100 Zeichen lang sein.")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Die Beschreibung ist erforderlich.")]
        [StringLength(500, ErrorMessage = "Die Beschreibung darf maximal 500 Zeichen lang sein.")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Die Kategorie ist erforderlich.")]
        public int? CategoryId { get; set; }
        [Required(ErrorMessage = "Die Priorität ist erforderlich.")]
        public TicketPriority? Priority { get; set; }
        public int? EmployeeId { get; set; }
        public List<TicketPriority> Priorities { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Employee> Employees { get; set; } = new();
    }
}
