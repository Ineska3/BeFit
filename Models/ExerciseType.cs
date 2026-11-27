using System.ComponentModel.DataAnnotations;

namespace BeFit.Models
{
    public class ExerciseType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nazwa musi mieć od 3 do 50 znaków.")]
        [Display(Name = "Nazwa ćwiczenia", Description = "Wpisz czytelną nazwę, np. Wyciskanie sztangi.")]
        public string Name { get; set; } = string.Empty;
    }
}