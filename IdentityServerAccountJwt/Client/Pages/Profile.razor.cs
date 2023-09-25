using IdentityServerAccountJwt.Client.Services;
using IdentityServerAccountJwt.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace IdentityServerAccountJwt.Client.Pages
{
    public partial class Profile
    {
        public ApplicationUser ApplicationUser { get; set; }=new ApplicationUser();
        [Inject]
        public IAuthenticationRegister authenticationRegister { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        [Parameter]
        public string Username { get; set; }
        protected override async Task OnInitializedAsync()
        {
           ApplicationUser=await authenticationRegister.GetUserByName(Username);

        }
        private void GoToChangePassword()
        {
            navigationManager.NavigateTo($"/changepassword/{Username}");
        }
    }
}
