using Microsoft.AspNetCore.Mvc;
using TopKnow.Management.Client.HttpClients;
using TopKnow.Management.Client.ViewModels;
using TopKnow.Management.Client.Helpers;

namespace TopKnow.Management.Client.Controllers
{
	public class UserController : Controller
	{
		private readonly ManagementApi managementApi;

		public UserController(ManagementApi managementApi)
        {
			this.managementApi = managementApi;
		}
        public async Task<IActionResult> Admins()
		{
			var admins = await managementApi.SendGetRequest<Result<List<AdminUserRequestOutput>>>("api/users/admins?Page=1&Size=10");
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
