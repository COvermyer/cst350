using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RegisterAndLoginApp.Models;

namespace RegisterAndLoginApp.Filters
{
	public class AdminCheckFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			string userInfo = context.HttpContext.Session.GetString("User");
			if (userInfo == null)
			{
				// No user is logged in, redirect to login
				context.Result = new RedirectResult("/User/Index");
				return;
			}

			// Check if user is an admin
			try
			{
				UserModel user = ServiceStack.Text.JsonSerializer.DeserializeFromString<UserModel>(userInfo);
				if (!user.Groups.Contains("Admin"))
				{ // User is not an admin, redirect to login
					context.Result = new RedirectResult("/User/Index");
					return;
				}
			}
			catch
			{ // Any errors in deserialization of session info, redirect to login
				context.Result = new RedirectResult("/User/Index");
				return;
			}
		}
	}
}
