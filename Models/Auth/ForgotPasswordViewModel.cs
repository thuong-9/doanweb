using System.ComponentModel.DataAnnotations;

namespace EasyCode.Models.Auth;

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public bool Submitted { get; set; }
}
