using System.ComponentModel.DataAnnotations;

namespace ServiceDesk.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name ist erforderlich")]
        [StringLength(50, ErrorMessage = "Maximal 50 Zeichen")]
        [Display(Name = "Name")]
        public string? Name { get; set; } 

        [StringLength(200)]
        [Display(Name = "Beschreibung")]
        public string? Description { get; set; }
    }
}