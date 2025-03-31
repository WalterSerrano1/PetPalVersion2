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
                // You can access user info from claims
                var username = User.Identity.Name;
                ViewBag.Username = username;

                //get pets for current user
                var userPets = await _context.Pet.Where(p => p.UserId == userId).ToListAsync();
                return View(userPets);  
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