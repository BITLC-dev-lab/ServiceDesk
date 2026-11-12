using System.ComponentModel.DataAnnotations;

namespace ServiceDesk.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name ist erforderlich")]
        [StringLength(50, ErrorMessage = "Maximal 50 Zeichen")]
        public string Name { get; set; } = string.Empty;
        [StringLength(200)]
        public string? Description { get; set; }
    }
}