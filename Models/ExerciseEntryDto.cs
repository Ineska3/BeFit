using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExerciseEntryDto
    {
        [Display(Name = "Typ ćwiczenia")]
        public int ExerciseTypeId { get; set; }

        [Display(Name = "Sesja treningowa")]
        public int WorkoutSessionId { get; set; }

        [Required(ErrorMessage = "Obciążenie jest wymagane.")]
        [Range(0.1, 1000, ErrorMessage = "Obciążenie musi wynosić od 0.1 do 1000 kg.")]
        [Display(Name = "Obciążenie (kg)", Description = "Podaj wagę w kilogramach.")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Liczba serii jest wymagana.")]
        [Range(1, 100, ErrorMessage = "Liczba serii musi wynosić od 1 do 100.")]
        [Display(Name = "Liczba serii", Description = "Ile serii wykonałeś?")]
        public int Sets { get; set; }

        [Required(ErrorMessage = "Liczba powtórzeń jest wymagana.")]
        [Range(1, 1000, ErrorMessage = "Liczba powtórzeń musi wynosić od 1 do 1000.")]
        [Display(Name = "Powtórzenia w serii", Description = "Ile powtórzeń w jednej serii?")]
        public int Reps { get; set; }
    }
}