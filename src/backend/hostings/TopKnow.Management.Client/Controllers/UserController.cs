using Microsoft.AspNetCore.Mvc;
using TopKnow.Management.Client.HttpClients;
using TopKnow.Management.Client.ViewModels;

namespace TopKnow.Management.Client.Controllers;

public class UserController : Controller
{
    private readonly ManagementApi managementApi;

    public UserController(ManagementApi managementApi)
    {
        this.managementApi = managementApi;
    }
    public async Task<IActionResult> Admins()
    {
        var admins = await managementApi.SendGetRequest<List<AdminUserRequestOutput>>("api/users/admins?Page=1&Size=10");
        return View(admins);
    }

    //ÖDEV
    public IActionResult Players()
    {
        return View();
    }

    //ÖDEV
    public IActionResult Externals()
    {
        return View();
    }

    public IActionResult NewAdmin()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewAdmin(CreateNewAdminViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await managementApi.SendPostRequest<bool, CreateNewAdminViewModel>("api/users/create-admin", model);
            if (result is not null && result.IsSuccess)
            {
                return RedirectToAction(nameof(Admins));
            }
            return View(nameof(NewAdmin));
        }
        return View(nameof(NewAdmin));
    }
}
