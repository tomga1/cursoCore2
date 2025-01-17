using System.ComponentModel.DataAnnotations;

namespace cursoCore2API.Models
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? nombre { get; set; }
    }
}
