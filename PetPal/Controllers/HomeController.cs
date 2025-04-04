using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PetPal.Models;
using Microsoft.AspNetCore.Authorization;
using PetPal.Data;
using PetManagement.Controllers;
using static AspNetCoreGeneratedDocument.Views_Home_Index;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;  // Add this at the top of your file

namespace PetPal.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext ctx)
        {
            _context = ctx;
        }

        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdClaim, out int userId))
            {
				var model = new DashboardViewModel
				{
					Pets = await _context.Pet
				 .Where(p => p.UserId == userId)
				 .ToListAsync(),

					Appointments = await _context.Appointments
				 .Where(a => a.AppointmentDateTime >= DateTime.Today && a.Pet.UserId == userId)
				 .Include(a => a.Pet)
				 .OrderBy(a => a.AppointmentDateTime)
				 .Take(5)
				 .ToListAsync(),

					Trainings = await _context.Training
				 .Where(t => t.Pet.UserId == userId)
				 .Include(t => t.Pet)
				 .OrderBy(t => t.TrainingDate)
				 .Take(5)
				 .ToListAsync()
				};
				return View("Index", model);
			}

            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            // If user is already logged in, redirect to index
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(IndexAsync));
            }

            return View(new LoginViewModel());
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            // If user is already logged in, redirect to index
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(IndexAsync));
            }

            return View(new RegisterViewModel());
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}