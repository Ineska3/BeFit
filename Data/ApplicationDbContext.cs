using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BeFit.Models;

namespace BeFit.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ExerciseType> ExerciseTypes { get; set; } = null!;
        public DbSet<WorkoutSession> WorkoutSessions { get; set; } = null!;
        public DbSet<ExerciseEntry> ExerciseEntries { get; set; } = null!;
    }
}