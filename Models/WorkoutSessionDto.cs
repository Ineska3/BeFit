using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class WorkoutSessionDto
    {
        [Required]
        [Display(Name = "Data rozpoczęcia")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "Data zakończenia")]
        public DateTime EndTime { get; set; }
    }
}