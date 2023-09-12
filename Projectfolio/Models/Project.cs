using System.ComponentModel.DataAnnotations;

namespace Projectfolio.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [MinLength(2, ErrorMessage = "Name too short! Enter again.")]
        [MaxLength(100)]
        [Display(Name = "Project Name")]
        [Required]
        public string? ProjectName { get; set; }
        
        [Required]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }
        
        [Display(Name = "Completion Date")]
        public DateTime? CompletionDate { get; set; }

        [Required]
        public string? Owner { get; set; }

        [Display(Name = "Project Status")]
        public string? ProjectStatus { get; set; }

        [Display(Name = "Technology")]
        // FK ref to technologies table
        public int TechnologyId { get; set; }

        // Reference to parent table
        public Technology? Technology { get; set; }


    }
}
