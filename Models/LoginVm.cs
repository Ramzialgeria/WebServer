using System.ComponentModel.DataAnnotations;

namespace ClientMonitoring.Web.Models;

public class LoginVm
{
    [Required]
    [Display(Name = "Nom d'utilisateur")]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Mot de passe")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Se souvenir de moi")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}