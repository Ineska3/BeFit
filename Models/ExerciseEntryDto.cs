using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExerciseEntryDto
    {
        [Display(Name = "Typ ćwiczenia")]
        public int ExerciseTypeId { get; set; }

        [Display(Name = "Sesja treningowa")]
        public int WorkoutSessionId { get; set; }

        [Required]
        [Display(Name = "Obciążenie (kg)")]
        public double Weight { get; set; }

        [Required]
        [Display(Name = "Liczba serii")]
        public int Sets { get; set; }

        [Required]
        [Display(Name = "Powtórzenia w serii")]
        public int Reps { get; set; }
    }
}