using IdentityServerAccountJwt.Client.Services;
using IdentityServerAccountJwt.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace IdentityServerAccountJwt.Client.Pages.AuthoriaztionPages
{
    public partial class Users
    {
        public List<ApplicationUser> applicationUsers { get; set; }=new List<ApplicationUser>();
        [Inject]
        public IAuthenticationRegister authenticationRegister { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
   
        protected override async Task OnInitializedAsync()
        {
            applicationUsers = await authenticationRegister.GetUsers();
        }
        public void GetUserByName(string username)
        {
            navigationManager.NavigateTo($"/getuserbyname/{username}");
        }   
        public void GetUserByEmail(string useremail)
        {
            navigationManager.NavigateTo($"/getuserbyemail/{useremail}");
        }
        public void GetUserRoles(string userid)
        {
            navigationManager.NavigateTo($"/userroles/{userid}");
        }
    }
}
