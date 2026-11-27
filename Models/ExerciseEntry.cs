using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExerciseEntry
    {
        public int Id { get; set; }

        [Display(Name = "Typ ćwiczenia")]
        public int ExerciseTypeId { get; set; }
        public ExerciseType? ExerciseType { get; set; }

        [Display(Name = "Sesja treningowa")]
        public int WorkoutSessionId { get; set; }
        public WorkoutSession? WorkoutSession { get; set; }

        [Required]
        [Range(0.1, 1000, ErrorMessage = "Obciążenie musi wynosić od 0.1 do 1000 kg.")]
        [Display(Name = "Obciążenie (kg)", Description = "Podaj wagę w kilogramach.")]
        public double Weight { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Liczba serii musi wynosić od 1 do 100.")]
        [Display(Name = "Liczba serii", Description = "Ile serii wykonałeś?")]
        public int Sets { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Liczba powtórzeń musi wynosić od 1 do 1000.")]
        [Display(Name = "Powtórzenia w serii", Description = "Ile powtórzeń w jednej serii?")]
        public int Reps { get; set; }

        public string? UserId { get; set; }
        public AppUser? User { get; set; }
    }
}