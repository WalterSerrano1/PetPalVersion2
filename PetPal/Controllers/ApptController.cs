using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using PetPal.Data;
using PetPal.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetPal.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly AppDbContext _context;

        public AppointmentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Appointment(int? id = null)
        {
            // Get the current user's ID
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToAction("Login", "Home");
            }

            // Get the user's pets for the dropdown
            var userPets = await _context.Pet
                .Where(p => p.UserId == userId)
                .Select(p => new SelectListItem
                {
                    Value = p.PetId.ToString(),
                    Text = p.PetName
                })
                .ToListAsync();

            // Pass the pets to the view
            ViewBag.Pets = userPets;

            // Create a new appointment model
            var appointment = new Appointment();

            // If a specific pet ID was provided, pre-select that pet
            if (id.HasValue)
            {
                appointment.PetId = id.Value;
                var pet = await _context.Pet.FirstOrDefaultAsync(p => p.PetId == id.Value);
                if (pet != null)
                {
                    appointment.PetName = pet.PetName;
                }
            }

            return View(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Appointment(Appointment model)
        {
            // Get the current user's ID
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return RedirectToAction("Login", "Home");
            }

            // Verify the pet belongs to the current user
            var pet = await _context.Pet
                .FirstOrDefaultAsync(p => p.PetId == model.PetId && p.UserId == userId);

            if (pet == null)
            {
                return NotFound();
            }

            // Explicitly set the pet name
            model.PetName = pet.PetName;

            // Remove previous PetName validation error
            ModelState.Remove("PetName");

            if (ModelState.IsValid)
            {
                // Add the appointment and save
                _context.Appointments.Add(model);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Appointment added successfully!";
                return RedirectToAction("Index", "Home", new { id = model.PetId });
            }

            // Repopulate pets dropdown if validation fails
            var userPets = await _context.Pet
                .Where(p => p.UserId == userId)
                .Select(p => new SelectListItem
                {
                    Value = p.PetId.ToString(),
                    Text = p.PetName
                })
                .ToListAsync();

            ViewBag.Pets = userPets;

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAppointment (int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            try
            {
                // Get the existing pet from database
                var existingAppt = await _context.Appointments.FindAsync(id);
                if (existingAppt == null)
                {
                    return NotFound();
                }

                // Update the database
                _context.Remove(existingAppt);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error deleting appointment: " + ex.Message);
            }

            // Redirect to the pet details page or index
            return RedirectToAction("Index", "Home");
        }

    }
}