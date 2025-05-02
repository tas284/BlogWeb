using System.ComponentModel.DataAnnotations;

namespace BlogWeb.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "E-mail is required")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    public string Email { get; set; }
}