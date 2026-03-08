using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TopKnow.Management.Client.HttpClients;
using TopKnow.Management.Client.ViewModels;

namespace TopKnow.Management.Client.Controllers;

public class AuthenticationController : Controller
{
    private readonly ManagementApi managementApi;

    public AuthenticationController(ManagementApi managementApi)
    {
        this.managementApi = managementApi;
    }
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginUserViewModel viewModel)
    {
        var result = await managementApi.SendPostRequest<LoginRequestOutput, LoginRequestInput>("api/auth/login", 
                                                                                                new LoginRequestInput 
                                                                                                { 
                                                                                                    Mail = viewModel.EMail, 
                                                                                                    Password = viewModel.Password 
                                                                                                });
        if (result.IsSuccess)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result.Value.Id.ToString()),
                new Claim(ClaimTypes.Name, result.Value.DisplayName),
                new Claim(ClaimTypes.Role, result.Value.UserType.GetHashCode().ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("Index", "Home");
        }
        return View();
    }
}
