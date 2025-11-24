using Microsoft.AspNetCore.Mvc;
using RegisterAndLoginApp.Models;
using RegisterAndLoginApp.Filters;

namespace RegisterAndLoginApp.Controllers
{
	public class UserController : Controller
	{
		static UserCollection users = new UserCollection();

		/// <summary>
		/// Returns the Index page to the View
		/// </summary>
		/// <returns></returns>
		public IActionResult Index()
		{
			return View();
		}

		/// <summary>
		/// ProcessLogin is a controller action to verify user credentials
		/// </summary>
		/// <param name="loginViewModel"></param>
		/// <returns></returns>
		public IActionResult ProcessLogin(string UserName, string password)
		{
			int userId = users.CheckCredentials(UserName, password);

			if (userId != -1)
			{
				// Save the user data in the session
				UserModel user = users.GetUserById(userId);
				string userJson = ServiceStack.Text.JsonSerializer.SerializeToString(user);
				HttpContext.Session.SetString("User", userJson);
				return View("LoginSuccess", users.GetUserById(userId));
			} else
			{
				return View("LoginFailure");
			}
		}

		/// <summary>
		/// Returns the MombersOnly view
		/// </summary>
		/// <returns></returns>
		[SessionCheckFilter]
		public IActionResult MembersOnly()
		{
			return View();
		}

		/// <summary>
		/// Logs out the stored Session user and returns to the Index
		/// </summary>
		/// <returns></returns>
		[SessionCheckFilter]
		public IActionResult Logout()
		{
			HttpContext.Session.Remove("User");
			return View("Index");
		}

		/// <summary>
		/// Controller method tro call the registration form into the view
		/// </summary>
		/// <returns></returns>
		public IActionResult Register()
		{
			return View(new RegisterViewModel());
		}

		public IActionResult ProcessRegister(RegisterViewModel registerViewModel)
		{
			// Create a blank user model
			UserModel user = new UserModel();

			// Add the username and hash the password for the new UserModel
			user.Username = registerViewModel.Username;
			user.SetPassword(registerViewModel.Password);
			
			// iterate through the selected groups and add them to the user model
			user.Groups = "";
			foreach (var group in registerViewModel.Groups)
			{
				if (group.IsSelected)
				{
					user.Groups += group.GroupName + ",";
				}
			}
			user.Groups = user.Groups.Trim(',');
			
			// Add the new user to the collection
			users.AddUser(user);

			//System.Diagnostics.Debug.WriteLine("NEW USER:");
			//System.Diagnostics.Debug.WriteLine(user.ToString());

   //         System.Diagnostics.Debug.WriteLine("SAVED USERS:");
			//foreach (UserModel u in users.GetAllUsers())
			//{
   //             System.Diagnostics.Debug.WriteLine(u.ToString());
   //         }

            // return to the login page.
            return View("Index");
		}
	}
}
