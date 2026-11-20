using System.ComponentModel.DataAnnotations;

namespace ServiceDesk.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name ist erforderlich")]
        [StringLength(200, ErrorMessage = "Der Kategoriename darf maximal 50 Zeichen lang sein")]
        public string Name { get; set; } = string.Empty;
    }
}