using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization; // NOWE

namespace BeFit.Controllers
{
    [Authorize] // Zabezpiecza cały kontroler - tylko dla zalogowanych
    public class WorkoutSessionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public WorkoutSessionsController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private string GetUserId() => _userManager.GetUserId(User)!;

        // Pokazuje TYLKO sesje zalogowanego użytkownika
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var sessions = _context.WorkoutSessions.Where(s => s.UserId == userId);
            return View(await sessions.ToListAsync());
        }

        // Weryfikuje czy sesja należy do użytkownika
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            
            var userId = GetUserId();
            var workoutSession = await _context.WorkoutSessions
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
                
            if (workoutSession == null) return NotFound();
            return View(workoutSession);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkoutSessionDto dto)
        {
            if (ModelState.IsValid)
            {
                var session = new WorkoutSession
                {
                    StartTime = dto.StartTime,
                    EndTime = dto.EndTime,
                    UserId = GetUserId()
                };
                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            
            var userId = GetUserId();
            var workoutSession = await _context.WorkoutSessions
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
                
            if (workoutSession == null) return NotFound();
            return View(workoutSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,EndTime,UserId")] WorkoutSession workoutSession)
        {
            if (id != workoutSession.Id) return NotFound();
            
            // Upewniamy się, że nie edytujemy cudzej sesji
            if (workoutSession.UserId != GetUserId()) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workoutSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutSessionExists(workoutSession.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(workoutSession);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            
            var userId = GetUserId();
            var workoutSession = await _context.WorkoutSessions
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
                
            if (workoutSession == null) return NotFound();
            return View(workoutSession);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = GetUserId();
            var workoutSession = await _context.WorkoutSessions
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
                
            if (workoutSession != null)
            {
                _context.WorkoutSessions.Remove(workoutSession);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutSessionExists(int id) => _context.WorkoutSessions.Any(e => e.Id == id);
    }
}