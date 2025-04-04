using Microsoft.AspNetCore.Mvc;

namespace PetPal.Controllers
{
	public class CalendarController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
