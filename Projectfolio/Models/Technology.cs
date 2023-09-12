using System.ComponentModel.DataAnnotations;

namespace Projectfolio.Models
{
    public class Technology
    {
        public int TechnologyId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Display(Name="Release Year")]
        [Required]
        public int? ReleaseYear { get; set; }

        public List<Project>? Projects { get; set; }
    }
}
