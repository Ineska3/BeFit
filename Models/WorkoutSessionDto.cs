using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class WorkoutSessionDto
    {
        [Required(ErrorMessage = "Data rozpoczęcia jest wymagana.")]
        [Display(Name = "Data rozpoczęcia", Description = "Podaj datę i godzinę rozpoczęcia treningu.")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Data zakończenia jest wymagana.")]
        [Display(Name = "Data zakończenia", Description = "Podaj datę i godzinę zakończenia treningu.")]
        public DateTime EndTime { get; set; }
    }
}