using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class WorkoutSession : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Data rozpoczęcia", Description = "Podaj datę i godzinę rozpoczęcia treningu.")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "Data zakończenia", Description = "Podaj datę i godzinę zakończenia treningu.")]
        public DateTime EndTime { get; set; }

        public string? UserId { get; set; }
        public AppUser? User { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartTime >= EndTime)
            {
                yield return new ValidationResult(
                    "Data zakończenia musi być późniejsza niż data rozpoczęcia.",
                    new[] { nameof(EndTime) });
            }
        }
    }
}