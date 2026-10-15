using ClientMonitoring.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClientMonitoring.Web.Controllers;

public class TestController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public TestController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> Identity()
    {
        var email = "admin@clientmonitoring.local";
        var password = "Admin1234";

        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return Content("USER NOT FOUND");

        var result = await _signInManager.PasswordSignInAsync(
            user.UserName!,
            password,
            false,
            false);

        return Content(
            $"Succeeded = {result.Succeeded}\n" +
            $"LockedOut = {result.IsLockedOut}\n" +
            $"NotAllowed = {result.IsNotAllowed}\n" +
            $"RequiresTwoFactor = {result.RequiresTwoFactor}");
    }
}