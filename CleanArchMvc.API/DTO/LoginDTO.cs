using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.API.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid format email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [StringLength(20, ErrorMessage = "The {0} must be a least {2} and at max {1} characters long", MinimumLength = 6)]
    public string Password { get; set; }
}
