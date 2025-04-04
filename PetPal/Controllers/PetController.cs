using Microsoft.AspNetCore.Mvc;
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

        public IActionResult PetDetails()
		{
			return View();
		}
		public IActionResult Appointment()
		{
			return View();
		}
		public IActionResult Schedule()
		{
			return View();
		}
		public IActionResult Training()
		{
			return View();
		}

		//Get action for displaying add pet form
		public IActionResult AddPet()
		{
			return View();
		}

		//POST action for handling form submission
		[HttpPost]
		public async Task<IActionResult> AddPet (Pet pet, IFormFile? ImageFile)
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


    }
}