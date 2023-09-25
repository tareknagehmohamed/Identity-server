using Blazored.LocalStorage;
using IdentityServerAccountJwt.Client.Authentication;
using IdentityServerAccountJwt.Client.Extentions;
using IdentityServerAccountJwt.Client.ResponseResult;
using IdentityServerAccountJwt.Client.Routs;
using IdentityServerAccountJwt.Shared.Dtos;
using IdentityServerAccountJwt.Shared.Models;
using IdentityServerAccountJwt.Shared.Pagination_Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Text.Json;

namespace IdentityServerAccountJwt.Client.Services
{
    public class AuthenticationRegister : IAuthenticationRegister
    {
        public HttpClient HttpClient { get; set; }
        private readonly JsonSerializerOptions _options;
        private readonly AuthenticationStateProvider _authenticationStates;
        private readonly ILocalStorageService _localStorageService;

        public AuthenticationRegister(HttpClient httpClient, AuthenticationStateProvider authenticationStatesClass,

           ILocalStorageService localStorageService )
        {
            HttpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive=true};
            _authenticationStates = authenticationStatesClass;
            _localStorageService = localStorageService;
        }

        public async Task<UserResponse> RegisterUser(UserRegister userRegister)
        {
            var result = await HttpClient.PostAsJsonAsync<UserRegister>("api/Accounts/CreateUser", userRegister);
            if (!result.IsSuccessStatusCode) {
            
            var message=await result.Content.ReadAsStringAsync();
                var msg = JsonSerializer.Deserialize<UserResponse>(message,_options);
                return msg;
            }
            return new UserResponse { IsSuccess=true };
        }

        public async Task<ResponseLogin> LoginUser(UserLogin userLogin)
        {
            var result = await HttpClient.PostAsJsonAsync<UserLogin>("api/Accounts/Login", userLogin);

            var message = await result.Content.ReadAsStringAsync();
            var msg = JsonSerializer.Deserialize<ResponseLogin>(message, _options);
            if (!result.IsSuccessStatusCode)
            {

                return msg;
            }
            await _localStorageService.SetItemAsync("AppToken",msg.Token);
            //((AuthenticationStatesClass)_authenticationStates).NotifyUserAuthentication(userLogin.UserName);
            ((AuthenticationStatesClass)_authenticationStates).NotifyUserAuthentication(msg.Token);
            HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue
            ("bearer",msg.Token);
            if (userLogin.IsRememberMe)
            {
                await _localStorageService.SetItemAsync("IsPersistentToken","IsPersistent");
            }
            else
            {
                await _localStorageService.RemoveItemAsync("IsPersistentToken");

            }
            return new ResponseLogin { IsSuccessLogin = true };

        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("AppToken");
            await _localStorageService.RemoveItemAsync("IsPersistentToken");

            ((AuthenticationStatesClass)_authenticationStates).NotifyUserLogout();
            HttpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
       return await HttpClient.GetFromJsonAsync<List<ApplicationUser>>("api/Accounts/GetUsers");
        }

        public async Task<ApplicationUser> GetUserByName(string name)
        {
            return await HttpClient.GetFromJsonAsync<ApplicationUser>($"api/Accounts/GetUsersByName?name={name}");

        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await HttpClient.GetFromJsonAsync<ApplicationUser>($"api/Accounts/GetUsersByEmail?email={email}");
          
        }

        public async Task<ApiReturnResult<List<ApplicationUser>>> GetUsersPagination(Pagination pagination)
        {
            var response = await HttpClient.PostAsJsonAsync(UserEndPoints.ListPagination, pagination);
            if (response.IsSuccessStatusCode)
            {
                var value = await response.ToResult<IdentityServerAccountJwt.Shared.GenaricResponse.ApiReturnObj<List<ApplicationUser>>>();
                return new()
                {

                    Result = value.ReturnValue,
                    Status = value.Status,
                    TotalPaginationCount = value.TotalPaginationCount
                };
            }
            else
            {
                return new()
                {
                    Result = null,
                    Status =IdentityServerAccountJwt.Shared.Enums.ApiReturnStatus.InternalError,
                    TotalPaginationCount = 0
                };
            }

        }
    }
}
