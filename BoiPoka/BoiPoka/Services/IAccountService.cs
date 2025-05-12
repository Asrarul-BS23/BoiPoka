using BoiPoka.ViewModels;

namespace BoiPoka.Services;

public interface IAccountService
{
    Task<(bool success, IEnumerable<string> errors)> RegisterAsync(RegisterViewModel model);
    Task<(bool success, string errorMessage)> LoginAsync(LoginViewModel model, HttpContext httpContext);
    Task LogoutAsync();
    Task<(bool success, string errorMessage)> ChangePasswordAsync(ChangePasswordViewModel model);
    Task<string> VerifyEmailAsync(VerifyEmailViewModel model);
}
