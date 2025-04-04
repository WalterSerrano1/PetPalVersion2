using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetPal.Data;
using PetPal.Models;
using System.Security.Claims;

namespace PetManagement.Controllers
{
	public class PetsController : Controller
	{
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Constructor with dependency injection
        public PetsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult PetDetails(int id)
		{
			var pet = _context.Pet.FirstOrDefault(p => p.PetId == id);
			return View(pet);
		}
	
		// Get action for displaying add/edit pet form
		public async Task<IActionResult> AddPet(int? id)
        {
            // If id is provided, we're in edit mode
            if (id.HasValue)
            {
                // Find the pet by id
                var pet = await _context.Pet.FindAsync(id.Value);

                // If pet doesn't exist, return not found
                if (pet == null)
                {
                    return NotFound();
                }

                // Pass the pet to the view for editing
                ViewBag.EditMode = true;
                return View(pet);
            }

            // If no id is provided, use add mode
            ViewBag.EditMode = false;
            return View(new Pet()); // Pass an empty pet model
        }

        //POST action for handling form submission
        [HttpPost]
		public async Task<IActionResult> AddPet(Pet pet, IFormFile? ImageFile)
		{
			if (ModelState.IsValid)
			{
                //get user id from authentication claims object
                string userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

                //parse to int for pet id
                if (int.TryParse(userIdClaim, out int userId))
                {
                    pet.UserId = userId;
                }
                else
                {
                    //Show error message in page error <div>
                    ModelState.AddModelError("", "Authentication error occurred. Please log in again.");
                    return View("AddPet", pet);
                }

                // Handle the image file if provided
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    // Create directory if it doesn't exist
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                    // Create a unique filename
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save the file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    // Store the relative path in the database
                    pet.ImageUrl = "/images/" + uniqueFileName;
                }
                else
                {
                    // Set a default image if none provided
                    pet.ImageUrl = "/images/default-pet.jpg";
                }

                // Add pet to database
                _context.Pet.Add(pet);
                await _context.SaveChangesAsync();

                // Redirect to the index page
                return RedirectToAction("Index", "Home");
            }

            // If fail redisplay form
            ModelState.AddModelError("", "Save failed. Please try again.");
            return View("AddPet", pet);
        }

        // Helper method to check if a pet exists
        private bool PetExists(int id)
        {
            return _context.Pet.Any(e => e.PetId == id);
        }

        [HttpPost]
        public async Task<IActionResult> EditPet(int id, Pet pet, IFormFile? ImageFile)
        {
            if (id != pet.PetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Get the existing pet from database
                    var existingPet = await _context.Pet.FindAsync(id);
                    if (existingPet == null)
                    {
                        return NotFound();
                    }

                    // Update the properties from the form
                    existingPet.PetName = pet.PetName;
                    existingPet.PetType = pet.PetType;
                    existingPet.PetBreed = pet.PetBreed;
                    existingPet.PetAge = pet.PetAge;
                    existingPet.PetBirthday = pet.PetBirthday;
                    existingPet.PetGender = pet.PetGender;

                    // Handle the image file if provided
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                        // Create directory if it doesn't exist
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // Create a unique filename
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // Save the file
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(fileStream);
                        }

                        // Update the image URL
                        existingPet.ImageUrl = "/images/" + uniqueFileName;
                    }

                    // Update the database
                    _context.Update(existingPet);
                    await _context.SaveChangesAsync();

                    // Redirect to the pet details page or index
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error updating pet: " + ex.Message);
                }
            }

            // redisplay form
            ViewBag.EditMode = true;
            return View("AddPet", pet);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePet(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
                try
                {
                    // Get the existing pet from database
                    var existingPet = await _context.Pet.FindAsync(id);
                    if (existingPet == null)
                    {
                        return NotFound();
                    }

                    // Update the database
                    _context.Remove(existingPet);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error deleting pet: " + ex.Message);
                }

            // Redirect to the pet details page or index
            return RedirectToAction("Index", "Home");
        }


    }
}