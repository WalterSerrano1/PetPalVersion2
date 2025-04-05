using Microsoft.AspNetCore.Mvc;
using PetPal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System;
using PetPal.Data;

namespace PetPal.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        //Inject AppDbContext
        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        //Handles Login request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //Check if Credentials are valid and redirect back to login if not
            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/Login.cshtml", model);
            }

            // Find user
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.Username);

            // Check if user exists
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View("~/Views/Home/Login.cshtml", model);
            }

            // Verify password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
            if (!isPasswordValid)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View("~/Views/Home/Login.cshtml", model);
            }

            // Create claims for the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            // Create identity
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Sign in the user
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties { IsPersistent = model.RememberMe });

            // Store user ID in session for easy access
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("Username", user.UserName);

            return RedirectToAction("Index", "Home");
        }

        //Handle user registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //checks if user credentials are valid and if not, redirect to register page
            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/Register.cshtml", model);
            }

            // Check if username already exists
            if (await _context.Users.AnyAsync(u => u.UserName == model.Username))
            {
                ModelState.AddModelError("", "Username already exists");
                return View("~/Views/Home/Register.cshtml", model);
            }

            // Hash the password using Bcrypt
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // Create new user
            var user = new User
            {
                UserName = model.Username,
                Password = passwordHash
            };

            // Save to database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Redirect to login
            TempData["SuccessMessage"] = "Registration successful! Please log in.";
            return RedirectToAction("Login", "Home");
        }

        //Handles logout
        [HttpPost]
        [Authorize] //protected route
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Sign out
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Clear session
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Home");
        }
    }
}