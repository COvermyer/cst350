using AppointmentScheduler.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentScheduler.Controllers
{
	public class AppointmentController : Controller
	{
		// GET: Appointment
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult ShowAppointmentDetails(AppointmentModel appointmentModel)
		{
			// Check custom validation results
			if (!ModelState.IsValid)
			{ // if the IsValid property fires as a failure, return to same view with model to show validation errors
				return View("Index", appointmentModel);
			}


			return View(appointmentModel);
		}
	}
}
