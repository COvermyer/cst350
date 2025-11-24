using ButtonGrid.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace ButtonGrid.Controllers
{
	public class ButtonController : Controller
	{
		static List<ButtonModel> buttons = new List<ButtonModel>();
		static string[] btnImgs = { "buttonBlue.png", "buttonRed.png", "buttonYellow.png", "buttonGreen.png" };

		public ButtonController()
		{
			// create a list of random buttons
			if (buttons.Count == 0)
			{
				for (int i = 0; i < 25; i++)
				{
					int number = RandomNumberGenerator.GetInt32(0, 4);
					buttons.Add(new ButtonModel(i, number, btnImgs[number]));
				}
			}
		}

		public IActionResult Index()
		{
			return View(buttons);
		}

		public IActionResult ButtonClick(int id)
		{
			ButtonModel button = buttons.FirstOrDefault(b => b.Id == id);
			if (button != null)
			{
				button.ButtonState = (button.ButtonState + 1) % 4;
				button.ButtonImage = btnImgs[button.ButtonState];
			}
			return RedirectToAction("Index");
		}

		public IActionResult PartialPageUpdate(int id)
		{
			ButtonModel button = buttons.FirstOrDefault(b => b.Id == id);
			if (button != null)
			{
				button.ButtonState = (button.ButtonState + 1) % 4;
				button.ButtonImage = btnImgs[button.ButtonState];
			}
			return PartialView("_Button", button);
		}

		public IActionResult RightClickPartialPageUpdate(int id)
		{
			ButtonModel button = buttons.FirstOrDefault(b => b.Id == id);
			if (button != null)
			{
				button.ButtonState = (button.ButtonState + 3) % 4;
				button.ButtonImage = btnImgs[button.ButtonState];
			}
			return PartialView("_Button", button);
		}
	}
}
