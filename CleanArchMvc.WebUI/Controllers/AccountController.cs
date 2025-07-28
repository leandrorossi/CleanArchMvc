using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers;

public class AccountController(IAuthenticate authenticate) : Controller
{
    private readonly IAuthenticate _authenticate = authenticate;

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var result = await _authenticate.Register(model.Email, model.Password);

        if (!result)
        {
            ModelState.AddModelError(string.Empty, "Invalid register attempt");
            return View(model);
        }

        return Redirect("/");
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl)
    {
        return View(new LoginViewModel()
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var result = await _authenticate.Authenticate(model.Email, model.Password);

        if (!result)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return View(model);
        }

        if (string.IsNullOrEmpty(model.ReturnUrl))
        {
            return RedirectToAction("Index", "Home");
        }

        return Redirect(model.ReturnUrl);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _authenticate.Logout();
        return Redirect("/Account/Login");
    }
}
