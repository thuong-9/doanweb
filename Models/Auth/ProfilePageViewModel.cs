namespace EasyCode.Models.Auth;

public class ProfilePageViewModel
{
    public string AvatarUrl { get; set; } = string.Empty;
    public string AvatarText { get; set; } = "U";

    public ProfileUpdateViewModel Update { get; set; } = new();
    public ChangePasswordViewModel Password { get; set; } = new();

    public string? StatusMessage { get; set; }
}
