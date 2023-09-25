using Blazored.LocalStorage;
using IdentityServerAccountJwt.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace IdentityServerAccountJwt.Client.Authentication
{
    public class AuthenticationStatesClass : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationState _anonymous;
        public AuthenticationStatesClass(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _localStorageService.GetItemAsync<string>("IsPersistentToken") != "IsPersistent")
            {
                await _localStorageService.RemoveItemAsync("AppToken");
            }
            //authToken
            var token =await _localStorageService.GetItemAsync<string>("AppToken");
            if (string.IsNullOrWhiteSpace(token))
                return _anonymous;
            _httpClient.DefaultRequestHeaders.Authorization=new System.Net.Http.Headers.AuthenticationHeaderValue("bearer",
                token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(
                JwtParser.ParseClaimsFromJwt(token),"jwtAuthType"
                )
                ));

        }
        public void NotifyUserAuthentication(string name)
        {
            //var authenticateUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
            //{
            //    new Claim(ClaimTypes.Name, name)
            //}, "jwtAuthType"));
            var authenticateUser = new ClaimsPrincipal(new ClaimsIdentity(
                
                JwtParser.ParseClaimsFromJwt(name), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticateUser));
            NotifyAuthenticationStateChanged(authState);
        }
        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);

        }
    }
}
