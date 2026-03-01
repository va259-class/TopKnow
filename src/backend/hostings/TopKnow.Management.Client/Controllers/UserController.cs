using Microsoft.AspNetCore.Mvc;

namespace TopKnow.Management.Client.Controllers
{
	public class UserController : Controller
	{
		public IActionResult Admins()
		{
			return View();
		}

		public IActionResult Players()
		{
			return View();
		}

		public IActionResult Externals()
		{
			return View();
		}
	}
}
