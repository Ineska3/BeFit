namespace BeFit.Models
{
    public class ExerciseStat
    {
        public string ExerciseName { get; set; } = string.Empty;
        public int TimesPerformed { get; set; }
        public int TotalReps { get; set; }
        public double AverageWeight { get; set; }
        public double MaxWeight { get; set; }
    }
}