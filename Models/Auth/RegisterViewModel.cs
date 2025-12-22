using System.ComponentModel.DataAnnotations;

namespace EasyCode.Models.Auth;

public class RegisterViewModel
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    public string? ReturnUrl { get; set; }
}
