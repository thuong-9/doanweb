using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EasyCode.Models.Auth;

public class ProfileUpdateViewModel
{
    [Required]
    [MaxLength(256)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public string Email { get; set; } = string.Empty;

    public IFormFile? AvatarFile { get; set; }
}
