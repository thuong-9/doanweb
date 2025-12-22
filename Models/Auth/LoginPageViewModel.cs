namespace EasyCode.Models.Auth;

public class LoginPageViewModel
{
    public bool IsSignup { get; set; }

    public LoginViewModel Login { get; set; } = new();

    public RegisterViewModel Register { get; set; } = new();
}
