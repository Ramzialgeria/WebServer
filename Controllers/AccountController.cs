using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ClientMonitoring.Web.Models;
using IdentityAppUser = ClientMonitoring.Web.Domain.Identity.AppUser;

namespace ClientMonitoring.Web.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityAppUser> _userManager;
    private readonly SignInManager<IdentityAppUser> _signInManager;

    public AccountController(
        UserManager<IdentityAppUser> userManager,
        SignInManager<IdentityAppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        return View(new LoginVm
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVm model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var result = await _signInManager.PasswordSignInAsync(
            model.UserName,
            model.Password,
            model.RememberMe,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            if (!string.IsNullOrWhiteSpace(model.ReturnUrl) &&
                Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(
            string.Empty,
            "Nom d'utilisateur ou mot de passe incorrect.");

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
