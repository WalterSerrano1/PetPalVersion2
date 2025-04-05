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

        //Get AppDbContext via constructor injection
        public HomeController(AppDbContext ctx)
        {
            _context = ctx;
        }

        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            // Extract the user ID from the claims principal
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Attempt to parse the user ID from the claim
            if (int.TryParse(userIdClaim, out int userId))
            {
                // Create a new dashboard view model to hold all the user's data
                var model = new DashboardViewModel
				{
                    // Get all pets belonging to the current user
                    Pets = await _context.Pet
				 .Where(p => p.UserId == userId)
				 .ToListAsync(),

                    // Includes the full pet data and orders by date
                    Appointments = await _context.Appointments
                 .Where(a => a.Pet.UserId == userId)    //filter by userId
                 .Include(a => a.Pet)
                 .OrderBy(a => a.AppointmentDateTime) //order by date
                 .Take(5)   //5 most recent appointments
                 .ToListAsync(),

                    // Get the 5 most recent training sessions for any of the user's pets
                    Trainings = await _context.Training
				 .Where(t => t.Pet.UserId == userId)
				 .Include(t => t.Pet)
				 .OrderBy(t => t.TrainingDate)
				 .Take(5)
				 .ToListAsync()
				};

                // Return the fully populated dashboard view
                return View("Index", model);
			}

            //redirect to login if error
            return RedirectToAction("Login", "Home");
        }

        // Pass loginViewModel to Login page
        [HttpGet]
        [AllowAnonymous] //allow unauthenticaed access
        public IActionResult Login()
        {
            // If user is already logged in, redirect to index
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(IndexAsync));
            }

            //return login form with viewmodel
            return View(new LoginViewModel());
        }

        //pass registerViewModel to Register page
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
            // Display access denied view when authorization fails
            return View();
        }

        // Prevents caching of error page
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Display error view with request ID for troubleshooting
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}