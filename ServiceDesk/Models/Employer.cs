using System.ComponentModel.DataAnnotations;

namespace ServiceDesk.Models
{
    public class Employer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vorname ist erforderlich")]
        [StringLength(50)]
        [Display(Name = "Vorname")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nachname ist erforderlich")]
        [StringLength(50)]
        [Display(Name = "Nachname")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-Mail ist erforderlich")]
        [EmailAddress(ErrorMessage = "Ungültige E-Mail")]
        [StringLength(100)]
        [Display(Name = "E-Mail")]
        public string Email { get; set; } = string.Empty;

    }
}
