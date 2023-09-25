using IdentityServerAccountJwt.Client.Pages.AuthoriaztionPages;
using IdentityServerAccountJwt.Shared.Dtos;
using IdentityServerAccountJwt.Shared.Dtos.Administrations;
using IdentityServerAccountJwt.Shared.Dtos.UsersRoles;
using System.Net.Http.Json;
using System.Text.Json;

namespace IdentityServerAccountJwt.Client.Services
{
    public class UserRole : IUserRole
    {
        public HttpClient HttpClient { get; set; }
        private readonly JsonSerializerOptions _options;

        public UserRole(HttpClient httpClient)
        {
            HttpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        }
        public async Task<UserRolesDto> GetUserRoles(string userid)
        {
            return await HttpClient.GetFromJsonAsync<UserRolesDto>($"api/UserRoles/GetUserRoles/{userid}");
        }

        public async Task AddUserRole(UserRolesDto userRolesDto)
        {
             await HttpClient.PostAsJsonAsync<UserRolesDto>($"api/UserRoles/AddRemoveUserRole",userRolesDto);
        }

        public async Task<UserResponse> ChangePassword(ChangePasswordDto changePasswordDto)
        {
          var result=  await HttpClient.PutAsJsonAsync($"api/UserRoles/ChangePasswordUser", changePasswordDto);
            if (!result.IsSuccessStatusCode )
            {
                var message = await result.Content.ReadAsStringAsync();
                var msg = JsonSerializer.Deserialize<UserResponse>(message, _options);
                return msg;
            }
            return new UserResponse { IsSuccess = true };

        }

        public async Task ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var result = await HttpClient.PostAsJsonAsync<ForgotPasswordDto>("api/UserRoles/ForgetPassword", forgotPasswordDto);
            //if (!result.IsSuccessStatusCode)
            //{
            //    var message = await result.Content.ReadAsStringAsync();
            //    var msg = JsonSerializer.Deserialize<UserResponse>(message, _options);
            //    return msg;
            //}
            //return new UserResponse { IsSuccess = true };
        }

        public async Task<UserResponse> ForgotPasswordTarek(ForgotPasswordDto forgotPasswordDto)
        {
            var result = await HttpClient.PostAsJsonAsync<ForgotPasswordDto>("api/UserRoles/ForgotPasswordTarek", forgotPasswordDto);
            if (!result.IsSuccessStatusCode)
            {
                var message = await result.Content.ReadAsStringAsync();
                var msg = JsonSerializer.Deserialize<UserResponse>(message, _options);
                return msg;
            }
            return new UserResponse { IsSuccess = true };
        }

        public async Task ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var result = await HttpClient.PostAsJsonAsync<ResetPasswordDto>("api/UserRoles/ResetPassword", resetPasswordDto);
            //if (!result.IsSuccessStatusCode)
            //{
            //    var message = await result.Content.ReadAsStringAsync();
            //    var msg = JsonSerializer.Deserialize<UserResponse>(message, _options);
            //    return msg;
            //}
            //return new UserResponse { IsSuccess = true };

        }
    }
}
