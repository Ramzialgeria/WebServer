using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
namespace ClientMonitoring.Web.Controllers;
[Authorize]
public class HomeController:Controller
{
    public IActionResult Index()=>View();
}
