using IdentityServerAccountJwt.Client.Services;
using IdentityServerAccountJwt.Shared.Dtos;
using Microsoft.AspNetCore.Components;

namespace IdentityServerAccountJwt.Client.Pages.AuthoriaztionPages
{
    public partial class LogoutUser
    {
        [Inject]
        public IAuthenticationRegister authorizationregister { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await authorizationregister.Logout();
            navigationManager.NavigateTo("/Login");
        }
    }
}
