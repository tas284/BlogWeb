using System.ComponentModel.DataAnnotations;

namespace BlogWeb.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Username is required")]
    [EmailAddress(ErrorMessage = "E-mail is invalid")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}