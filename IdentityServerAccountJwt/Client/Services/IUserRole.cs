using IdentityServerAccountJwt.Shared.Dtos;
using IdentityServerAccountJwt.Shared.Dtos.UsersRoles;

namespace IdentityServerAccountJwt.Client.Services
{
    public interface IUserRole
    {
        Task<UserRolesDto> GetUserRoles(string userid);
        Task AddUserRole(UserRolesDto userRolesDto);
        Task<UserResponse> ChangePassword(ChangePasswordDto changePasswordDto);
        Task ForgotPassword(ForgotPasswordDto forgotPasswordDto);
        Task<UserResponse> ForgotPasswordTarek(ForgotPasswordDto forgotPasswordDto);
        Task ResetPassword(ResetPasswordDto resetPasswordDto);
    }
}
