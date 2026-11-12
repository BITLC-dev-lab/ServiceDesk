using System.ComponentModel.DataAnnotations;

namespace ServiceDesk.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vorname ist erforderlich")]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Nachname ist erforderlich")]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "E-Mail ist erforderlich")]
        [EmailAddress(ErrorMessage = "Ungültige E-Mail")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
    }
}
