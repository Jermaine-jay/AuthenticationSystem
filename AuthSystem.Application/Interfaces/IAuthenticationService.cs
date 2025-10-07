using AuthSystem.Application.Dtos.Request;
using AuthSystem.Application.Helpers;

namespace AuthSystem.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task BanUser(BanUserViewModel model);
        Task<string> ChangePassword(ChangePasswordRequest request);
        Task<string> ForgotPassword(ForgotPasswordRequest request);
        Task<UserViewModel> GetUser(string UserId);
        Task<JwtToken> Login(LoginRequestView login);
        Task<JwtToken> Register(RegistrationRequest register);
        Task<string> ResetPassword(ResetPasswordRequest request);
        Task UpdateUser(UpdateUserRequest request);
    }
}
