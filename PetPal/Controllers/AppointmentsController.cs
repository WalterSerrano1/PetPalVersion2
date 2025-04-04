using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetPal.Models;
using PetPal.Data;


namespace PetPal.Controllers
{
	public class AppointmentsController : Controller
	{
		private AppDbContext _context { get; set; }
		public AppointmentsController(AppDbContext context)
		{
			_context = context;
		}

		
	}
}
