using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.API.DTO;

public class RegisterDTO
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid format email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]    
    public string Password { get; set; }
    
    [DataType(DataType.Password)]
    [DisplayName("Confirm Password")]
    [Compare("Password", ErrorMessage = "Passwords dont`t match")]
    public string ConfirmPassword { get; set; }
}
